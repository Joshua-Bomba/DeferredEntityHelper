using DeferredEntityHelper.Futures.Callback;
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
        protected IFutureCallback<T> _futureCallback;
        protected bool _fullyResolved;
        protected bool _entityResolved;
        protected IFuture _next;
        public Future(IFutureCallback<T> callback, IDependencyResolver dependencyResolver): base()
        {
            _fullyResolved = false;
            _entityResolved = false;
            _dependencyResolver = dependencyResolver;
            _futureCallback = callback;
            _next = null;
        }

        public override bool Resolved => _fullyResolved;

        protected abstract void UpdateModel(T model);

        public override async Task<T> ForceResolveAndGetItem()
        {
            if (!_fullyResolved)
            {
                await _dependencyResolver.TriggerResolve();
            }
            return this.GetItem();
        }

        protected IEnumerable<IFutureEvent>? GetUnresolvedDepedencies()
        {
            if(!_futureCallback.DepedenciesResolved())
            {
                List<IFutureEvent> ev = new List<IFutureEvent>(_futureCallback.GetUnResolvedElements());
                ev.Add(this);
                return ev;
            }
            return null;
        }

        protected virtual bool ProcessNextElement()
        {
            if (!_entityResolved)
                return false;

            if (_next != null)
            {
                return FutureResolve(_next);
            }
            if (_futureCallback == null)
            {
                _fullyResolved = true;
                return false;
            }

            return true;
        }


        protected bool FutureResolve(IFuture f)
        {
            if (f.Resolved)
            {
                if (f is IFuture<T> fWithITem)
                {
                    UpdateModel(fWithITem.GetItem());
                }
                _fullyResolved = true;
                return true;
            }
            else
            {
                _next = f;
                return false;
            }

        }


        public async Task<IEnumerable<IFutureEvent>?> Process()
        {
            if (_fullyResolved)
                return null;

            if (!ProcessNextElement())
            {
                return new IFutureEvent[] { this };
            }
            IEnumerable<IFutureEvent>? deps =  GetUnresolvedDepedencies();
            if (deps != null)
                return deps;
            IFuture f = await _futureCallback.Callback(this);
            _futureCallback = null;
            if (!FutureResolve(f))
            {
                if (f is IFutureEvent ev)
                {
                    return new IFutureEvent[] { this, ev };
                }
                else
                {
                    return new IFutureEvent[] { this };
                }
            }
            return null;
        }

        public virtual void DependencyResolvedTrigger() => _entityResolved = true;


    }
}
