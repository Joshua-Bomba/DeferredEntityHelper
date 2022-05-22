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
        private IFutureCallback<T> _callback;
        private PotentialFuture<T> _next;
        public FutureUnDetermined(IFutureCallback<T> callback, IDependencyResolver efHelper) : base(null, efHelper)
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
                PotentialFuture<T> p = await _callback.Callback(this);
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
