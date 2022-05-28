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
        protected PotentialFuture<T> _next;
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


        protected virtual IEnumerable<IFutureEvent>? HandleNextElement(PotentialFuture<T> add)
        {
            _next = add;
            if (add is IFutureEvent ev)
            {
                return new IFutureEvent[] { this, ev };
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        protected virtual bool ProcessNextElement()
        {
            if (!_entityResolved)
                return false;

            if (_next != null)
            {
                if (_next.Resolved)
                {
                    UpdateModel(_next.GetItem());
                    _fullyResolved = true;
                    _next = null;
                }
                else
                {
                    return false;
                }
            }
            if (_futureCallback == null)
            {
                _fullyResolved = true;
                return false;
            }

            return true;
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
            PotentialFuture<T> p = await _futureCallback.Callback(this);
            _futureCallback = null;
            if (!p.Resolved)
            {
                return HandleNextElement(p);                
            }
            else
            {
                UpdateModel(p.GetItem());
                _fullyResolved = true;
            }
            return null;
        }

        public virtual void DependencyResolvedTrigger() => _entityResolved = true;


    }
}
