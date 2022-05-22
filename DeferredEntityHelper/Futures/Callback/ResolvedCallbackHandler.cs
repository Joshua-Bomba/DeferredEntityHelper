﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures.Callback
{
    public class ResolvedCallbackHandler<T> : IFutureCallback<T> where T : class
    {
        Func<T, Task> _elementResolved;
        public ResolvedCallbackHandler(Func<T,Task> elementResolved)
        {
            _elementResolved = elementResolved;
        }
        public async Task<IFuture<T>> Callback(IFuture<T>? context)
        {
            T? resolvedElement = context?.GetItem();
            if(resolvedElement != null)
            {
                if(_elementResolved != null)
                {
                    await _elementResolved(resolvedElement);
                }
            }

            return context;
        }

        public bool DepedenciesResolved() => true;
    }
}
