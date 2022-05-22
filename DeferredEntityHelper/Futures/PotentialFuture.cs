using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures
{
    public abstract class PotentialFuture<T> : IFuture<T> where T : class
    {
        protected T _data;
        public PotentialFuture(T data)
        {
            _data = data;
        }

        public T GetItem()
        {
            return _data;
        }

        object IFuture.GetItem() => this.GetItem();

        public abstract bool Resolved { get; }

        public abstract Task<T> ForceResolveAndGetItem();


        public static implicit operator PotentialFuture<T>(T t) => new FutureWrapper<T>(t);
    }
}
