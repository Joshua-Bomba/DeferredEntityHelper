using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures.Callback
{
    public abstract class BaseFutureCallBackWithParams<T,CB> : IFutureCallback<T> where T : class
    {
        protected IPotentialFuture[] _e;
        protected CB _cb;
        private byte _enumCount;
        public BaseFutureCallBackWithParams(CB cb) 
        {
            _cb = cb;
            _enumCount = 0;
        }

        public virtual IFutureCallback<T> AttachItems(params IPotentialFuture[] items)
        {
            _e = items;
            return this;
        }

        protected virtual TResult GetItem<TResult>() where TResult : class => ((IFuture<TResult>)_e[_enumCount++]).GetItem();

        public abstract Task<IFuture<T>> Callback();
        async Task<IFuture<T>> IFutureCallback<T>.Callback(IFuture<T> context) => await Callback();

        public bool DepedenciesResolved() => !_e.Any(x => !x.Resolved);


    }
}
