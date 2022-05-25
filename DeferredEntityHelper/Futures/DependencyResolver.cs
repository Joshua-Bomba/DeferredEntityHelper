using DeferredEntityHelper.Futures.Callback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures
{
    public class DependencyResolver : IDependencyResolver
    {
        private HashSet<IFutureEvent> _def;
        public DependencyResolver()
        {
            _def = new HashSet<IFutureEvent>();
        }

        public virtual FutureDetermined<TProp> AddUnresolvedElement<TProp>(TProp el, IFutureCallback<TProp> callback) where TProp : class
        {
            FutureDetermined<TProp> save = new FutureDetermined<TProp>(el, callback, this);
            _def.Add(save);
            return save;
        }

        public virtual void AddUnresolvedElement(IFutureEvent f)
        {
            _def.Add(f);
        }

        public virtual async Task<bool> TriggerResolve()
        {
            HashSet<IFutureEvent> refs = _def;
            _def = new HashSet<IFutureEvent>();
            foreach (IFutureEvent p in refs)
                p.DependencyResolvedTrigger();
            foreach (IFutureEvent p in refs)
            {
                IEnumerable<IFutureEvent>? unresolved = await p.Process();
                if(unresolved != null)
                {
                    foreach(IFutureEvent f in unresolved)
                        _def.Add(f);
                }
            }
                
            return !_def.Any();
        }

        public virtual async Task TriggerResolves(Func<Task> resolveOperation)
        {
            do
            {
                await resolveOperation();
            }while(!await TriggerResolve());
        }

        public virtual async Task<PotentialFuture<TProp>> WaitForPromises<TProp>(IFutureCallback<TProp> callback) where TProp : class
        {
            if (!callback.DepedenciesResolved())
            {
                Future<TProp> save = new FutureUnDetermined<TProp>(callback, this);
                this._def.Add(save);
                return save;
            }

            return await callback.Callback(null);
        }
    }
}
