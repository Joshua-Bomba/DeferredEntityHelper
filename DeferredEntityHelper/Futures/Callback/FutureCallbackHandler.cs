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
        private IFuture[] _wait;
        public FutureCallbackHandler(Func<Task<PotentialFuture<T>>> f, params IFuture[] wait)
        {
            _func = f;
            _wait = wait;
        }
        public async Task<PotentialFuture<T>> Callback() => await _func();

        public bool DepedenciesResolved() => !_wait.Any(x => !x.Resolved);
    }
}
