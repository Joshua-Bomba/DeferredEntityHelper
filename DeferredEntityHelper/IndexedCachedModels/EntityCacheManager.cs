using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeferredEntityHelper.Futures;

namespace DeferredEntityHelper.IndexedCachedModels
{
    public interface IEntityCacheManagerContextTracking
    {
        void EnqueContextTask(ValueTask v);
    }

    public class EntityCacheManager : IEntityCacheManagerContextTracking
    {
        private Dictionary<Type, IEntityCache> _cachedItems;
        private IBaseEntityHelper _context;
        private List<ValueTask> _contextTasks;
        public EntityCacheManager(IBaseEntityHelper context)
        {
            _context = context;
            _cachedItems = new Dictionary<Type, IEntityCache>();
            _contextTasks = new List<ValueTask>();
        }
        public virtual async ValueTask<ICachedModelAccess<TKey, TValue>> GetCachedIndexedDictionary<TKey, TValue>(Func<TValue, TKey> indexer) where TValue : class where TKey : notnull
        {
            Type tValType = typeof(TValue);
            EntityCache<TValue> ec;
            if (!_cachedItems.ContainsKey(tValType))
            {
                ec = new EntityCache<TValue>(_context,this);
                _cachedItems[tValType] = ec;
            }
            else
            {
                ec = (EntityCache<TValue>)_cachedItems[tValType];
            }

            return await ec.GetByIndexer(indexer);
        }

        public void NewEntityAdded<TProp>(IFutureDetermined<TProp> e) where TProp : class
        {
            Type entityType = typeof(TProp);
            if (_cachedItems.ContainsKey(entityType))
            {
                _cachedItems[entityType].Add(e);
            }
        }

        public async ValueTask EnsureReadersAreFinished()
        {
            if (_contextTasks.Any())
            {
                foreach (ValueTask task in _contextTasks)
                {
                    await task;
                }
                _contextTasks.Clear();
            }

        }

        void IEntityCacheManagerContextTracking.EnqueContextTask(ValueTask v) => _contextTasks.Add(v);
    }
}
