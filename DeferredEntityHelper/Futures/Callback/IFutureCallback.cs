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

        Task<PotentialFuture<T>> Callback(IFuture<T>? context);
    }
}
