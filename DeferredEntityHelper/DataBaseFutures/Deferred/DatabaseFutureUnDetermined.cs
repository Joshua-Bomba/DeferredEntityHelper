using DeferredEntityHelper.DataBaseFutures.Callback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.DataBaseFutures
{
    public class DatabaseFutureUnDetermined<T> : DatabaseFuture<T> where T : class
    {
        private IFutureCallback<T> _callback;
        private PotentialFuture<T> _next;
        public DatabaseFutureUnDetermined(IFutureCallback<T> callback, IPostSaveOperations efHelper) : base(null, efHelper)
        {
            _callback = callback;
            _next = null;
        }

        public override void SavedChangesTriggered()
        {

        }

        public async override Task Process()
        {
            if (_next != null)
            {
                if (_next.Resolved)
                {
                    _data = _next.GetItem();
                    _resolved = true;
                }
                else
                {
                    this._efHelper.AddUnresolvedElement(this);
                }
            }
            else if (!_callback.DepedenciesResolved())
            {
                this._efHelper.AddUnresolvedElement(this);//we will have it go around again till the promises we are waiting on are resolved
            }
            else
            {
                PotentialFuture<T> p = await _callback.Callback();
                _callback = null;
                if (!p.Resolved)
                {
                    _next = p;
                    this._efHelper.AddUnresolvedElement(this);
                }
                else
                {
                    _data =p.GetItem();
                    _resolved = true;
                }
            }
        }
    }
}
