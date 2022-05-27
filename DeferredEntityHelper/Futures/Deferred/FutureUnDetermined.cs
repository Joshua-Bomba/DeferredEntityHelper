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
        private T _model;
        public FutureUnDetermined(IFutureCallback<T> callback, IDependencyResolver dependencyResolver) : base(callback, dependencyResolver)
        {
            _model = null;
        }

        public override T GetItem() => _model;

        protected override void UpdateModel(T model) => _model = model;
    }
}
