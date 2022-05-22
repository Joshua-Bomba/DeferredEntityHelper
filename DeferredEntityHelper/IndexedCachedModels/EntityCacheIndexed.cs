using DeferredEntityHelper.Futures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.IndexedCachedModels
{
    public class EntityCacheIndexed<TModel, TModelAccessKey> : Dictionary<TModelAccessKey, IFutureDetermined<TModel>>, IEntityCacheIndexed<TModel>, ICachedModelAccess<TModelAccessKey, TModel> where TModel : class where TModelAccessKey : notnull
    {
        private Func<TModel, TModelAccessKey> _keyGetter;
        private ValueTask _setupTask;
        public EntityCacheIndexed(Func<TModel, TModelAccessKey> keyGetter) : base()
        {
            _keyGetter = keyGetter;
        }

        private async ValueTask _SetupTask(DbContext context)
        {
            IAsyncEnumerator<TModel> en = context.Set<TModel>().GetAsyncEnumerator();
            while (await en.MoveNextAsync())
            {
                
                this[_keyGetter(en.Current)] = IFutureDetermined.Wrap(en.Current);
            }
        }

        public ValueTask SetupCacheSetFromDb(DbContext context)
        {
            _setupTask = _SetupTask(context);
            return _setupTask;
        }

        public void Add(IFutureDetermined<TModel> entity)
        {
            this[_keyGetter(entity.GetItem())] = entity;
        }

        private async ValueTask _SetupCacheFromRelated(IEntityCacheIndexed<TModel> relatedSet)
        {
            await relatedSet.Finished();
            foreach (IFutureDetermined<TModel> entity in relatedSet.GetData())
            {
                this[_keyGetter(entity.GetItem())] = entity;
            }
        }

        public void SetupCacheFromRelated(IEntityCacheIndexed<TModel> relatedSet)
            => _setupTask = _SetupCacheFromRelated(relatedSet);

        public IEnumerable<IFutureDetermined<TModel>> GetData() => this.Values;

        public async ValueTask Finished() => await _setupTask;
    }
}
