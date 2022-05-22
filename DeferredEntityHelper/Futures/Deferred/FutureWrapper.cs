﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures
{
    public class FutureWrapper<T> : PotentialFuture<T>, IFutureDetermined<T> where T : class
    {

        public FutureWrapper(T t) : base(t) { }
        public override bool Resolved => true;

        public async override Task<T> ForceResolveAndGetItem() => _data;

        PotentialFuture<T> IFutureDetermined<T>.AsPotentialFuture() => this;
    }
}
