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
    public class DependencyBridge<T,U> : IFutureCallback<T>,IFutureCallback<U>, IFuture<U> where T : class where U : class
    {
        private Func<T, Task<PotentialFuture<U>>> _func;
        private PotentialFuture<U> _future;
        private T? _resolvedElement;
        public DependencyBridge(Func<T, Task<PotentialFuture<U>>> func)
        {
            _func = func;
        }

        bool IFuture.Resolved => _future != null ?_future.Resolved : false;

        object IFuture.GetItem() => GetItem();

        public U GetItem() => _future.GetItem();

        async Task<PotentialFuture<T>> IFutureCallback<T>.Callback(IFuture<T>? context)
        {
            _resolvedElement = context?.GetItem();
            if (_resolvedElement != null && _func != null)
            {
                _future = await _func(_resolvedElement);
            }
            return _resolvedElement;
        }

        bool IFutureCallback<T>.DepedenciesResolved() => true;

        bool IFutureCallback<U>.DepedenciesResolved() => _future != null ? _future.Resolved : false;

        async Task<PotentialFuture<U>> IFutureCallback<U>.Callback(IFuture<U>? context) => _future;
    }
}
