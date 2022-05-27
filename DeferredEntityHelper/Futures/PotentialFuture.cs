using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures
{
    public abstract class PotentialFuture<T> : IFuture<T> where T : class
    {
        public PotentialFuture() { }

        public abstract T GetItem();

        object IFuture.GetItem() => this.GetItem();

        public abstract bool Resolved { get; }

        public abstract Task<T> ForceResolveAndGetItem();

        public static implicit operator PotentialFuture<T>(T t) => new FutureWrapper<T>(t);
    }
}
