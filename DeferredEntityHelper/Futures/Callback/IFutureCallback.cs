using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures.Callback
{
    public interface IFutureCallback<T> where T : class
    {
        bool DepedenciesResolved();

        IEnumerable<IFutureEvent> GetUnResolvedElements();

        Task<PotentialFuture<T>> Callback(IFuture<T>? context);
    }
}
