using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures
{
    public abstract class Future<T> : PotentialFuture<T>, IFutureEvent where T : class
    {
        protected IDependencyResolver _dependencyResolver;
        protected bool _resolved;
        public Future(T data, IDependencyResolver dependencyResolver) : base(data)
        {
            _resolved = false;
            _dependencyResolver = dependencyResolver;
        }

        public override bool Resolved => _resolved;

        public override async Task<T> ForceResolveAndGetItem()
        {
            if (!_resolved)
            {
                await _dependencyResolver.TriggerResolve();
            }
            return _data;
        }

        public abstract Task Process();

        public virtual void DependencyResolvedTrigger() => _resolved = true;
    }
}
