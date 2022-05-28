using DeferredEntityHelper.Futures.Callback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures
{
    /// <summary>
    /// For ensuring something remains unresolved untill another callback is called
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DependencyBridge<T> : IFutureCallback<T>, IFuture where T : class
    {
        private Func<T, Task<IFuture>> _func;
        private IFuture _future;
        public DependencyBridge(Func<T, Task<IFuture>> func)
        {
            _func = func;
            _future = null;
        }

        public bool Resolved => _future != null ?_future.Resolved : false;

        public object GetItem() => null;

        async Task<IFuture> IFutureCallback<T>.Callback(IFuture<T>? context)
        {
            T? resolvedElement = context?.GetItem();
            if (resolvedElement != null && _func != null)
            {
                return await _func(resolvedElement);
            }
            return context;
        }

        bool IFutureCallback<T>.DepedenciesResolved() => _future != null;

        IEnumerable<IFutureEvent> IFutureCallback<T>.GetUnResolvedElements()
        {
            if(_future != null&& _future is IFutureEvent f)
            {
                yield return f;
            }
        }
    }
}
