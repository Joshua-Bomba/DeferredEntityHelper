using DeferredEntityHelper.Futures.Callback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures
{
    public class FutureDetermined<T> : Future<T>, IFutureDetermined<T> where T : class
    {
        private T _data;
        public FutureDetermined(T data, IFutureCallback<T> callback, IDependencyResolver dependencyResolver) : base(callback, dependencyResolver)
        {
            _data = data;
        }

        public override T GetItem() => _data;

        protected override void UpdateModel(T model) => _data = model;

        PotentialFuture<T> IFutureDetermined<T>.AsPotentialFuture() => this;
    }
}
