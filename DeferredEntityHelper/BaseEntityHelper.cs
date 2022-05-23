﻿using DeferredEntityHelper.Futures;
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


    public abstract partial class BaseEntityHelper : DependencyResolver,  IAsyncDisposable 
    {
        protected EntityCacheManager _cacheManager;
        public BaseEntityHelper()
        {

        }

        protected virtual void SetupCacheManager()
        {
            _cacheManager = new EntityCacheManager(Context);
        }

        public abstract DbContext Context { get; }

        public virtual async Task<FutureDetermined<TProp>> AddEntityAsync<TProp>(TProp e, Func<TProp, Task> actionPostSave = null) where TProp : class
        {
            try
            {
                await this.Context.Set<TProp>().AddAsync(e);
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }

            FutureDetermined<TProp> det = this.AddUnresolvedElement(e, actionPostSave);
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

        public async ValueTask DisposeAsync()
        {
            await SaveChangesAsync();
        }
    }
}
