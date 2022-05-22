using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures
{
    public abstract class Future<T> : PotentialFuture<T>, IFutureEvent where T : class
    {
        protected IFuturePostResolveOperations _efHelper;
        protected bool _resolved;
        public Future(T data, IFuturePostResolveOperations efHelper) : base(data)
        {
            _resolved = false;
            _efHelper = efHelper;
        }

        public override bool Resolved => _resolved;

        public override async Task<T> ForceResolveAndGetItem()
        {
            if (!_resolved)
            {
                await _efHelper.TriggerFullSave();
            }
            return _data;
        }

        public abstract Task Process();

        public virtual void SavedChangesTriggered() => _resolved = true;
    }
}
