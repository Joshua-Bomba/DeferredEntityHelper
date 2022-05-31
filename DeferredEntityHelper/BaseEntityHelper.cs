using DeferredEntityHelper.Futures;
using DeferredEntityHelper.Futures.Callback;
using DeferredEntityHelper.IndexedCachedModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper
{

    public class BaseEntityHelper<T> : DependencyResolver,  IAsyncDisposable, IBaseEntityHelper where T : DbContext
    {
        protected T _context;
        protected EntityCacheManager _cacheManager;
        public BaseEntityHelper(T context)
        {
            _context = context;
            SetupCacheManager();
        }

        public virtual T Context => _context;

        protected virtual void SetupCacheManager()
        {
            _cacheManager = new EntityCacheManager(this);
        }

        public virtual async Task<FutureDetermined<TProp>> AddEntityAsync<TProp>(TProp e, Func<TProp, Task> actionPostSave) where TProp : class
            => await AddEntityAsync(e, new ResolvedCallbackHandler<TProp>(actionPostSave));

        public virtual async Task<FutureDetermined<TProp>> AddEntityAsync<TProp>(TProp e, IFutureCallback<TProp> cb= null) where TProp : class
        {
            try
            {
                await this.Context.Set<TProp>().AddAsync(e);
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }

            FutureDetermined<TProp> det = await this.AddUnresolvedElement(e, cb);
            _cacheManager.NewEntityAdded(det);
            return det;
        }

        public virtual async Task SaveChangesAsync()
        {
            await _cacheManager.EnsureReadersAreFinished();
            await TriggerResolves(async () => await this.Context.SaveChangesAsync());
        }

        public virtual async Task DeleteEntityAsync<TProp>(TProp e) where TProp : class
        {
            this.Context.Set<TProp>().Remove(e);
        }

        public virtual async ValueTask DisposeAsync()
        {
            await SaveChangesAsync();
        }

        public virtual IAsyncEnumerator<TProp> GetAllEntitiesOfType<TProp>(Func<IQueryable<TProp>, IQueryable<TProp>>? f = null) where TProp : class
        {
            if(f != null)
            {
                return this.Context.Set<TProp>().GetAsyncEnumerator();
            }
            else
            {
                return f(this.Context.Set<TProp>()).AsAsyncEnumerable().GetAsyncEnumerator();
            }
        }
    }
}
