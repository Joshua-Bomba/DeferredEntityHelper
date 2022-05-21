using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.DataBaseFutures.Callback
{
    public abstract class BaseFutureCallBackWithParams<T> : IFutureCallback<T> where T : class
    {
        private IFuture[] _wait;
        public BaseFutureCallBackWithParams(IFuture[] wait)
        {
            _wait = wait;
        }

        public abstract Task<PotentialFuture<T>> Callback();

        public bool DepedenciesResolved() => !_wait.Any(x => !x.Resolved);
    }
}
