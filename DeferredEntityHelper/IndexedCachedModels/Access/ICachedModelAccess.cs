using DeferredEntityHelper.Futures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.IndexedCachedModels
{
    public interface ICachedModelAccess<TKey,TValue> : IDictionary<TKey, IFutureDetermined<TValue>> where TValue : class {}
}
