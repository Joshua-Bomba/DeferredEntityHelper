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
        public FutureUnDetermined(IFutureCallback<T> callback, IDependencyResolver dependencyResolver) : base(null, dependencyResolver)
        {
            _callback = callback;
            _next = null;
        }

        public override void DependencyResolvedTrigger()
        {

        }

        public async override Task<IEnumerable<IFutureEvent>?> Process()
        {
            if (_next != null)
            {
                if (_next.Resolved)
                {
                    _data = _next.GetItem();
                    _resolved = true;
                    _next = null;
                }
                else
                {
                    return new IFutureEvent[] { this };
                }
            }
            else if (!_callback.DepedenciesResolved())
            {
                List<IFutureEvent> ev = new List<IFutureEvent> { this };
                ev.AddRange(_callback.GetUnResolvedElements());
            }
            else
            {
                PotentialFuture<T> p = await _callback.Callback(this);
                _callback = null;
                if (!p.Resolved)
                {
                    _next = p;
                    if(p is IFutureEvent ev)
                    {
                        return new IFutureEvent[] { this, ev };
                    }
                    else
                    {
                        throw new NotSupportedException();
                    }
                }
                else
                {
                    _data = p.GetItem();
                    _resolved = true;
                }
            }
            return null;
        }
    }
}
