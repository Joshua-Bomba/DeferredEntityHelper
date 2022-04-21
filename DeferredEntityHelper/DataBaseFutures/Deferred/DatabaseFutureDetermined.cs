using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.DataBaseFutures
{
    public class DatabaseFutureDetermined<T> : DatabaseFuture<T> where T : class
    {
        private Func<T, Task> _func;
        public DatabaseFutureDetermined(T key, Func<T, Task> postFunc, IPostSaveOperations efHelper) : base(key, efHelper)
        {
            _func = postFunc;
        }

        public override async Task Process()
        {
            Task? t = this._func?.Invoke(_data);
            if (t != null)
            {
                await t;
            }
        }
    }
}
