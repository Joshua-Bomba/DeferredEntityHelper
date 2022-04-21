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
        public EntityCacheIndexed(Func<TModel, TModelAccessKey> keyGetter) : base()
        {
            _keyGetter = keyGetter;
        }

        public async Task SetupCacheSetFromDb(DbContext context)
        {
            IAsyncEnumerator<TModel> en = context.Set<TModel>().GetAsyncEnumerator();
            while (await en.MoveNextAsync())
            {
                this[_keyGetter(en.Current)] = en.Current;
            }
        }

        public void Add(TModel entity)
        {
            this[_keyGetter(entity)] = entity;
        }

        public void SetupCacheFromRelated(IEntityCacheIndexed<TModel> relatedSet)
        {
            foreach (TModel entity in relatedSet.GetData())
            {
                this[_keyGetter(entity)] = entity;
            }
        }

        public IEnumerable<TModel> GetData() => this.Values;
    }
}
