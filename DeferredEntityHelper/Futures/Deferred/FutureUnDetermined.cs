using DeferredEntityHelper.Futures.Callback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures
{
    public class FutureUnDetermined<T> : Future<T> where T : class
    {
        public FutureUnDetermined(IFutureCallback<T> callback, IDependencyResolver dependencyResolver) : base(null,callback, dependencyResolver)
        {
        }

        public override void DependencyResolvedTrigger()
        {

        }
    }
}
