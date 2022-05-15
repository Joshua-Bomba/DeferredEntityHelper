using DeferredEntityHelper.DataBaseFutures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.IndexedCachedModels
{
    public class EntityCacheIndexed<TModel, TModelAccessKey> : Dictionary<TModelAccessKey, PotentialFuture<TModel>>, IEntityCacheIndexed<TModel>, ICachedModelAccess<TModelAccessKey, TModel> where TModel : class where TModelAccessKey : notnull
    {
        private Func<TModel, TModelAccessKey> _keyGetter;
        private Dictionary<TModelAccessKey, PotentialFuture<TModel>> _cache;
        private List<PotentialFuture<TModel>> _unresolved;
        private ValueTask _setupTask;
        public EntityCacheIndexed(Func<TModel, TModelAccessKey> keyGetter) : base()
        {
            _keyGetter = keyGetter;
            _unresolved = new List<PotentialFuture<TModel>>();
        }

        public void Add(PotentialFuture<TModel> entity)
        {
            if (entity.Resolved || entity is DatabaseFutureDetermined<TModel>)
            {
                TModel m = entity.GetCurrentItem();
                this[_keyGetter(m)] = entity;
            }
            else
            {
                _unresolved.Add(entity);
            }
        }

        private async ValueTask _SetupCacheFromRelated(IEntityCacheData<TModel> relatedSet)
        {
            await relatedSet.Finished();
            foreach (PotentialFuture<TModel> entity in relatedSet.GetData())
            {
                this.Add(entity);
            }
        }

        public void SetupCache(IEntityCacheData<TModel> relatedSet)
            => _setupTask = _SetupCacheFromRelated(relatedSet);

        public IEnumerable<PotentialFuture<TModel>> GetData() => this.Values;

        public async ValueTask Finished() => await _setupTask;
    }
}
