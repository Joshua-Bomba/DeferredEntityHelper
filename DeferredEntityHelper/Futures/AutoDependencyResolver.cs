using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures
{
    public class AutoDependencyResolver : DependencyResolver, IAutoDependencyResolver
    {
        private Func<Task> _triggerResolveOperations;
        public AutoDependencyResolver(Func<Task> triggerResolveOperation)
        {
            _triggerResolveOperations = triggerResolveOperation;
        }

        public async ValueTask DisposeAsync() => await this.TriggerResolves(_triggerResolveOperations);
    }
}
