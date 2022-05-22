using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures
{
    public class FutureDetermined<T> : Future<T> where T : class
    {
        private Func<T, Task> _func;
        public FutureDetermined(T key, Func<T, Task> postFunc, IDependencyResolver efHelper) : base(key, efHelper)
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
