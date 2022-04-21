using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.IndexedCachedModels
{
    public class EntityCacheManager
    {
        private Dictionary<Type, IEntityCache> _cachedItems;
        private DbContext _context;
        public EntityCacheManager(DbContext context)
        {
            _context = context;
            _cachedItems = new Dictionary<Type, IEntityCache>();
        }
        public virtual async Task<ICachedModelAccess<TKey, TValue>> GetCachedIndexedDictionary<TKey, TValue>(Func<TValue, TKey> indexer) where TValue : class where TKey : notnull
        {
            Type tValType = typeof(TValue);
            EntityCache<TValue> ec;
            if (!_cachedItems.ContainsKey(tValType))
            {
                ec = new EntityCache<TValue>(_context);
                _cachedItems[tValType] = ec;
            }
            else
            {
                ec = (EntityCache<TValue>)_cachedItems[tValType];
            }

            return await ec.GetByIndexer(indexer);
        }

        public void Add<TProp>(TProp e) where TProp : class
        {
            Type entityType = typeof(TProp);
            if (_cachedItems.ContainsKey(entityType))
            {
                _cachedItems[entityType].Add(e);
            }
        }

    }
}
