﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.DataBaseFutures
{
    public class PotentialFuture<T> : IFuture where T : class
    {
        protected T _data;
        public PotentialFuture(T data)
        {
            _data = data;
        }

        public T GetUnresolvedItem()
        {
            return _data;
        }

        public virtual bool Resolved => true;

        public virtual async Task<T> GetResult() => _data;

        public static implicit operator PotentialFuture<T>(T t) => new PotentialFuture<T>(t);

    }
}
