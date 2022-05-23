﻿using DeferredEntityHelper.Futures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace DeferredEntityHelper
{
    public class ConcurrentBaseEntityHelper<T> : BaseEntityHelper<T> where T : DbContext
    {
        private AsyncLock _lock;
        public ConcurrentBaseEntityHelper(T context) : base(context)
        {
            _lock = new AsyncLock();
        }


        public override async Task<FutureDetermined<TProp>> AddEntityAsync<TProp>(TProp e, Func<TProp, Task> actionPostSave = null)
        {
            using(await _lock.LockAsync())
            {
                return await base.AddEntityAsync(e, actionPostSave);
            }
        }

    }
}
