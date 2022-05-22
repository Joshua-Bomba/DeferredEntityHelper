using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures
{
    public interface IFuture<T> : IPotentialFuture where T : class
    {
        T GetItem();
        Task<T> ForceResolveAndGetItem();

    }
}
