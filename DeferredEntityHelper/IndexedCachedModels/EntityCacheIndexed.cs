using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.IndexedCachedModels
{
    public class EntityCacheIndexed<TModel, TModelAccessKey> : Dictionary<TModelAccessKey, TModel>, IEntityCacheIndexed<TModel>, ICachedModelAccess<TModelAccessKey,TModel> where TModel : class where TModelAccessKey : notnull
    {
        private Func<TModel, TModelAccessKey> _keyGetter;
        private ValueTask _setupTask;
        public EntityCacheIndexed(Func<TModel, TModelAccessKey> keyGetter) : base()
        {
            _keyGetter = keyGetter;
        }

        public void Add(TModel entity)
        {
            this[_keyGetter(entity)] = entity;
        }

        private async ValueTask _SetupCacheFromRelated(IEntityCacheData<TModel> relatedSet)
        {
            await relatedSet.Finished();
            foreach (TModel entity in relatedSet.GetData())
            {
                this[_keyGetter(entity)] = entity;
            }
        }

        public void SetupCache(IEntityCacheData<TModel> relatedSet)
            => _setupTask = _SetupCacheFromRelated(relatedSet);

        public IEnumerable<TModel> GetData() => this.Values;

        public async ValueTask Finished() => await _setupTask;
    }
}
