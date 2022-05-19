using DeferredEntityHelper.DataBaseFutures;
using DeferredEntityHelper.IndexedCachedModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper
{
    public class BaseEntityHelper<T> : BaseEntityHelper where T : DbContext
    {
        protected T _context;
        public BaseEntityHelper(T context)
        {
            _context = context;
            SetupCacheManager();
        }

        public override T Context => _context;
    }


    public abstract partial class BaseEntityHelper :  IAsyncDisposable, IPostSaveOperations 
    {
        private HashSet<IDatabaseFuture> _def;
        protected EntityCacheManager _cacheManager;
        public BaseEntityHelper()
        {
            _def = new HashSet<IDatabaseFuture>();
        }

        protected virtual void SetupCacheManager()
        {
            _cacheManager = new EntityCacheManager(Context);
        }

        public abstract DbContext Context { get; }

        public virtual async Task<PotentialFuture<TProp>> WaitForPromises<TProp>(IFutureCallback<TProp> callback) where TProp : class
        {
            if (!callback.DepedenciesResolved())
            {
                DatabaseFuture<TProp> save = new DatabaseFutureUnDetermined<TProp>(callback, this);
                this._def.Add(save);
                return save;
            }

            return await callback.Callback();
        }


        public virtual async Task<DatabaseFutureDetermined<TProp>> AddEntityAsync<TProp>(TProp e, Func<TProp, Task> actionPostSave = null) where TProp : class
        {
            _cacheManager.Add(e);
            await this.Context.Set<TProp>().AddAsync(e);
            DatabaseFutureDetermined<TProp> save = new DatabaseFutureDetermined<TProp>(e, actionPostSave, this);
            _def.Add(save);
            return save;
        }

        public virtual async Task SaveChangesAsync()
        {
            HashSet<IDatabaseFuture> refs;
            await _cacheManager.EnsureReadersAreFinished();
            do
            {
                await this.Context.SaveChangesAsync();
                //using (await _mutext.LockAsync())
                {
                    refs = _def;
                    _def = new HashSet<IDatabaseFuture>();
                }
                foreach (IDatabaseFuture p in refs)
                    p.SavedChangesTriggered();
                foreach (IDatabaseFuture p in refs)
                    await p.Process();
            } while (_def.Any());
        }

        public virtual async Task DeleteEntityAsync<TProp>(TProp e) where TProp : class
        {
            this.Context.Set<TProp>().Remove(e);
        }

        async Task IPostSaveOperations.TriggerFullSave()
            => await this.SaveChangesAsync();

        void IPostSaveOperations.AddUnresolvedElement(IDatabaseFuture f)
            => _def.Add(f);

        public async ValueTask DisposeAsync()
        {
            await SaveChangesAsync();
        }
    }
}
