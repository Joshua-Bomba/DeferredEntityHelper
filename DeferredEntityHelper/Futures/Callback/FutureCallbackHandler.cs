using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures.Callback
{
    public class FutureCallbackHandler<T> : IFutureCallback<T> where T : class
    {
        private Func<Task<PotentialFuture<T>>> _func;
        private IPotentialFuture[] _wait;
        public FutureCallbackHandler(Func<Task<PotentialFuture<T>>> f, params IPotentialFuture[] wait)
        {
            _func = f;
            _wait = wait;
        }
        public async Task<PotentialFuture<T>> Callback(IFuture<T> resolved) => await _func();

        public bool DepedenciesResolved() => !_wait.Any(x => !x.Resolved);
    }
}
