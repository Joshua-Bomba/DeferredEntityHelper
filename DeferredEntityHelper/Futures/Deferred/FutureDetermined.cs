using DeferredEntityHelper.Futures.Callback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures
{
    public class FutureDetermined<T> : Future<T>, IFutureDetermined<T> where T : class
    {
        public FutureDetermined(T key, IFutureCallback<T> callback, IDependencyResolver dependencyResolver) : base(key,callback, dependencyResolver)
        {

        }

        PotentialFuture<T> IFutureDetermined<T>.AsPotentialFuture() => this;
    }
}
