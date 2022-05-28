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
        private T? _resolvedElement;
        public DependencyBridge(Func<T, Task<IFuture>> func)
        {
            _func = func;
            _future = null;
        }

        bool IFuture.Resolved => _future != null ?_future.Resolved : false;

        object IFuture.GetItem() => _resolvedElement;

        async Task<PotentialFuture<T>> IFutureCallback<T>.Callback(IFuture<T>? context)
        {
            _resolvedElement = context?.GetItem();
            if (_resolvedElement != null && _func != null)
            {
                _future = await _func(_resolvedElement);
            }
            return _resolvedElement;
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
