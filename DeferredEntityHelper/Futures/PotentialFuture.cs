using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures
{
    public abstract class PotentialFuture<T> : IPotentialFuture where T : class
    {
        protected T _data;
        public PotentialFuture(T data)
        {
            _data = data;
        }

        //public T GetItem()
        //{
        //    return _data;
        //}

        public abstract bool Resolved { get; }

    }
}
