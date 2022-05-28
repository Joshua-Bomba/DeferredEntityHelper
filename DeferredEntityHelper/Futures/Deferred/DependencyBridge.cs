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
        private bool _resolved;
        private PotentialFuture<T> _f;
        public DependencyBridge()
        {
            _resolved = false;
        }

        public bool Resolved => _resolved;

        public object GetItem() => _f;

        async Task<PotentialFuture<T>> IFutureCallback<T>.Callback(IFuture<T>? context)
        {
            T? resolvedElement = context?.GetItem();
            _resolved = true;
            return resolvedElement;
        }

        bool IFutureCallback<T>.DepedenciesResolved() => _resolved;

        IEnumerable<IFutureEvent> IFutureCallback<T>.GetUnResolvedElements()
        {
            if(_f != null&&_f is IFutureEvent f)
            {
                yield return f;
            }
        }
    }
}
