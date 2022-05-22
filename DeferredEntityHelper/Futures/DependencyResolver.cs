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
        public void AddUnresolvedElement(IFutureEvent f)
        {
            _def.Add(f);
        }

        public async Task<bool> TriggerResolve()
        {
            HashSet<IFutureEvent> refs = _def;
            _def = new HashSet<IFutureEvent>();
            foreach (IFutureEvent p in refs)
                p.SavedChangesTriggered();
            foreach (IFutureEvent p in refs)
                await p.Process();
            return !_def.Any();
        }

        public async Task TriggerResolves(Func<Task> resolveOperation)
        {
            do
            {
                await resolveOperation();
            }while(await TriggerResolve());
        }

        public async Task<PotentialFuture<TProp>> WaitForPromises<TProp>(IFutureCallback<TProp> callback) where TProp : class
        {
            if (!callback.DepedenciesResolved())
            {
                Future<TProp> save = new FutureUnDetermined<TProp>(callback, this);
                this._def.Add(save);
                return save;
            }

            return await callback.Callback();
        }
    }
}
