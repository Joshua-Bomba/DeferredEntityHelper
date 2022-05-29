using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures.Callback
{
    public abstract class BaseFutureCallBackWithParams<T,CB> : IFutureCallback<T> where T : class
    {
        protected List<IFuture> _e;
        protected CB _cb;
        public BaseFutureCallBackWithParams(CB cb) 
        {
            _cb = cb;
        }

        public virtual BaseFutureCallBackWithParams<T,CB> AttachItems(params IFuture[] items)
        {
            if(_e == null)
            {
                _e = items.ToList();
            }
            else
            {
                _e.AddRange(items);
            }
            return this;
        }

        protected virtual TResult GetItem<TResult>(int index) where TResult : class => (TResult)_e[index].GetItem();

        public abstract Task<PotentialFuture<T>> Callback();

        async Task<IFuture> IFutureCallback<T>.Callback(IFuture<T> context) => await Callback();

        public bool DepedenciesResolved() => !_e.Any(x => !x.Resolved);

        public IEnumerable<IFutureEvent> GetUnResolvedElements()
        {
            return _e.Where(x => !x.Resolved).Where(x=>x is IFutureEvent).Cast<IFutureEvent>();
        }


    }
}
