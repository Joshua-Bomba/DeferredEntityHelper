using DeferredEntityHelper.DataBaseFutures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.IndexedCachedModels
{
    
    public interface IEntityCacheManager
    {
        IEntityCacheData<TProp> DbDataFetch<TProp>() where TProp : class;
    }

    public class EntityCacheManager : IEntityCacheManager
    {
        private Dictionary<Type, IEntityCache> _cachedItems;
        private DbContext _context;
        private ILoadFromDataBase? _loadFromDataBase;
        private ValueTask? _finish;
        public EntityCacheManager(DbContext context)
        {
            _context = context;
            _cachedItems = new Dictionary<Type, IEntityCache>();
            _loadFromDataBase = null;
            _finish = null;
        }
        public virtual async ValueTask<ICachedModelAccess<TKey, TValue>> GetCachedIndexedDictionary<TKey, TValue>(Func<PotentialFuture<TValue>, TKey> indexer) where TValue : class where TKey : notnull
        {
            Type tValType = typeof(TValue);
            EntityCache<TValue> ec;
            if (!_cachedItems.ContainsKey(tValType))
            {
                ec = new EntityCache<TValue>(this);
                _cachedItems[tValType] = ec;
            }
            else
            {
                ec = (EntityCache<TValue>)_cachedItems[tValType];
            }

            return await ec.GetByIndexer(indexer);
        }

        public void Add<TProp>(PotentialFuture<TProp> e) where TProp : class
        {
            Type entityType = typeof(TProp);
            if (_cachedItems.ContainsKey(entityType))
            {
                _cachedItems[entityType].Add(e);
            }
        }

        public async ValueTask EnsureReadersAreFinished()
        {
            if(_finish != null)
            {
                await _finish.Value;
            }
        }

        IEntityCacheData<TProp> IEntityCacheManager.DbDataFetch<TProp>()
        {
            LoadFromDataBase<TProp> loadRequest = new LoadFromDataBase<TProp>();
            if(_loadFromDataBase == null)
            {
                _loadFromDataBase = loadRequest;
                _finish = loadRequest.Process(_context);
            }
            else
            {
                _loadFromDataBase.Stack(loadRequest);
            }
            return loadRequest;
        }
    }
}
