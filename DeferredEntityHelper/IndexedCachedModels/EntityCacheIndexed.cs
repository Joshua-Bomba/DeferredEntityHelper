using Microsoft.EntityFrameworkCore;
using Nito.AsyncEx;
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
        private AsyncManualResetEvent _initMre;
        public EntityCacheIndexed(Func<TModel, TModelAccessKey> keyGetter) : base()
        {
            _keyGetter = keyGetter;
            _initMre = new AsyncManualResetEvent(false);
        }

        public async ValueTask SetupCacheSetFromDb(DbContext context)
        {
            IAsyncEnumerator<TModel> en = context.Set<TModel>().GetAsyncEnumerator();
            while (await en.MoveNextAsync())
            {
                this[_keyGetter(en.Current)] = en.Current;
            }
            _initMre.Set();
        }

        public void Add(TModel entity)
        {
            this[_keyGetter(entity)] = entity;
        }

        public async ValueTask SetupCacheFromRelated(IEntityCacheIndexed<TModel> relatedSet)
        {
            await foreach (TModel entity in relatedSet.GetData())
            {
                this[_keyGetter(entity)] = entity;
            }
        }

        public async IAsyncEnumerable<TModel> GetData()
        {
            await _initMre.WaitAsync();
            foreach(TModel t in this.Values)
            {
                yield return t;
            }
        }

        public async ValueTask<TModel?> AtKey(TModelAccessKey key)
        {
            await _initMre.WaitAsync();
            if (key != null && this.ContainsKey(key))
            {
                return this[key];
            }
            return null;
        }

        public IAsyncEnumerator<TModel> GetAsyncEnumerator(CancellationToken cancellationToken = default) => GetData().GetAsyncEnumerator();

        public async ValueTask<IDictionary<TModelAccessKey, TModel>> AsDictionary(TModelAccessKey key)
        {
            await _initMre.WaitAsync();
            return this;
        }
    }
}
