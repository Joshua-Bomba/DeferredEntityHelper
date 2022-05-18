using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.IndexedCachedModels
{
    public class EntityCache<T> : IEntityCache where T : class
    {
        private Dictionary<Type, IEntityCacheIndexed<T>> _cacheSets;
        private DbContext _dbContext;
        private IEntityCacheManagerContextTracking _contextTracking;

        public EntityCache(DbContext context, IEntityCacheManagerContextTracking contextTracking)
        {
            _cacheSets = new Dictionary<Type, IEntityCacheIndexed<T>>();
            _dbContext = context;
            _contextTracking = contextTracking;
        }

        public async ValueTask<ICachedModelAccess<TKey, T>> GetByIndexer<TKey>(Func<T, TKey> indexer) where TKey : notnull
        {
            Type keyType = typeof(TKey);

            EntityCacheIndexed<T, TKey> ecs;
            if (!_cacheSets.ContainsKey(keyType))
            {
                ecs = new EntityCacheIndexed<T, TKey>(indexer);
                bool anyCacheSets = _cacheSets.Any();
                _cacheSets[keyType] = ecs;
                if (anyCacheSets)
                {
                    IEntityCacheIndexed<T> otherSet = _cacheSets.First().Value;
                    ecs.SetupCacheFromRelated(otherSet);
                }
                else
                {
                    _contextTracking.EnqueContextTask(ecs.SetupCacheSetFromDb(_dbContext));
                }

            }
            else
            {
                ecs = (EntityCacheIndexed<T, TKey>)_cacheSets[keyType];
            }
            await ecs.Finished();
            return ecs;
        }

        public void Add(object t)
        {
            if (_cacheSets.Any())
            {
                T prop = (T)t;
                foreach (KeyValuePair<Type, IEntityCacheIndexed<T>> kv in _cacheSets)
                {
                    kv.Value.Add(prop);
                }
            }
        }
    }
}
