using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.DataBaseFutures
{
    public abstract class DatabaseFuture<T> : PotentialFuture<T>, IDatabaseFuture where T : class
    {
        protected IPostSaveOperations _efHelper;
        protected bool _resolved;
        public DatabaseFuture(T data, IPostSaveOperations efHelper) : base(data)
        {
            _resolved = false;
            _efHelper = efHelper;
        }

        public override bool Resolved => _resolved;

        void IDatabaseFuture.SavedChangesTriggered() => _resolved = true;

        public override async Task<T> GetResult()
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
