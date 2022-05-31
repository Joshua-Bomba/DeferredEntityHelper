using DeferredEntityHelper.Futures.Callback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nito.AsyncEx;
using System.Collections.Concurrent;

namespace DeferredEntityHelper.Futures
{
    public class DependencyResolver : IDependencyResolver
    {
        private ConcurrentDictionary<IFutureEvent,byte> _def;
        private ConcurrentDictionary<IFutureEvent,byte> _lastLoop;
        private const byte NPTR = default;
        public DependencyResolver()
        {
            _def = new ConcurrentDictionary<IFutureEvent,byte>();
            _lastLoop = null;
        }

        public virtual async  Task<FutureDetermined<TProp>> AddUnresolvedElement<TProp>(TProp el, IFutureCallback<TProp> callback) where TProp : class
        {
            FutureDetermined<TProp> save = new FutureDetermined<TProp>(el, callback, this);
            await AddUnresolvedElement(save);
            return save;
        }

        public virtual async Task AddUnresolvedElement(IFutureEvent f)
        {
            _def.TryAdd(f,NPTR);
        }

        public virtual async Task<bool> TriggerResolve()
        {
            ConcurrentDictionary<IFutureEvent,byte> refs = _def;
            _def = new ConcurrentDictionary<IFutureEvent,byte>();
            foreach (IFutureEvent p in refs.Keys)
                p.DependencyResolvedTrigger();
            foreach (IFutureEvent p in refs.Keys)
                await p.Process();
            bool any = _def.Any();


            if (any && _lastLoop != null && _def.Count == _lastLoop.Count)
            {
                throw new Exception("Dependency Not Resolved");
            }
            else if(any)
            {
                _lastLoop = _def;
                return false;
            }
            else
                return true;
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
                await this.AddUnresolvedElement(save);
                return save;
            }

            return await callback.Callback(null);
        }
    }
}
