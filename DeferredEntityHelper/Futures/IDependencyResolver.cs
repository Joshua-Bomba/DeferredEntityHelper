using DeferredEntityHelper.Futures.Callback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures
{
    public interface IDependencyResolver
    {
        Task<bool> TriggerResolve();

        Task TriggerResolves(Func<Task> resolveOperation);
        Task<FutureDetermined<TProp>> AddUnresolvedElement<TProp>(TProp el, IFutureCallback<TProp> callback) where TProp : class;
        Task AddUnresolvedElement(IFutureEvent f);
        Task<PotentialFuture<TProp>> WaitForPromises<TProp>(IFutureCallback<TProp> callback) where TProp : class;
    }
}
