using DeferredEntityHelper.DataBaseFutures;
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
        private IEntityCacheManager _cacheManager;

        public EntityCache(IEntityCacheManager cacheManager)
        {
            _cacheSets = new Dictionary<Type, IEntityCacheIndexed<T>>();
            _cacheManager = cacheManager;
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
                IEntityCacheData<T> otherSet;
                if (anyCacheSets)
                {
                    otherSet = _cacheSets.First().Value;
                }
                else
                {
                    otherSet = _cacheManager.DbDataFetch<T>();
                }
                 
                ecs.SetupCache(otherSet);
            }
            else
            {
                ecs = (EntityCacheIndexed<T, TKey>)_cacheSets[keyType];
            }
            await ecs.Finished();
            return ecs;
        }

        public void Add(IFuture t)
        {
            if (_cacheSets.Any())
            {
                PotentialFuture<T> prop = (PotentialFuture<T>)t;
                foreach (KeyValuePair<Type, IEntityCacheIndexed<T>> kv in _cacheSets)
                {
                    kv.Value.Add(prop);
                }
            }
        }
    }
}
