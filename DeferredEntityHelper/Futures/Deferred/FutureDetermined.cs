using DeferredEntityHelper.Futures.Callback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures
{
    public class FutureDetermined<T> : Future<T>, IFutureDetermined<T> where T : class
    {
        private IFutureCallback<T> _callback;
        public FutureDetermined(T key, IFutureCallback<T> callback, IDependencyResolver dependencyResolver) : base(key, dependencyResolver)
        {
            _callback = callback;
        }

        public T Item => _data;

        public override async Task Process()
        {
            Task? t = this._callback?.Callback(this);
            if (t != null)
            {
                await t;
            }
        }
    }
}
