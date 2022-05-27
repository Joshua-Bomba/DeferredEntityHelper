using DeferredEntityHelper.Futures;
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


        public override async IAsyncEnumerator<TProp> GetAllEntitiesOfType<TProp>(Func<IQueryable<TProp>,IQueryable<TProp>>? f = null)
        {
            TProp[] el;
            using (await _lock.LockAsync())
            {
                if (f == null)
                {
                    el = await this.Context.Set<TProp>().ToArrayAsync();
                }
                else
                    el = await f(this.Context.Set<TProp>()).ToArrayAsync();
            }

            foreach (TProp e in el)
                yield return e;   
        }

    }
}
