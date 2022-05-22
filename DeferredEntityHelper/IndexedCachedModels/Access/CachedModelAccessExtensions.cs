using DeferredEntityHelper.Futures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.IndexedCachedModels
{
    //Hmm not sure if some of this stuff is built in 
    public static class CachedModelAccessExtensions
    {
        public static async ValueTask<IFutureDetermined<TValue?>> AtKey<TKey,TValue>(this ValueTask<ICachedModelAccess<TKey, TValue>> a, TKey key) where TKey : notnull where TValue : class
        {
            ICachedModelAccess<TKey, TValue> c = await a;
            if(c != null&& c.ContainsKey(key))
            {
                return c[key];
            }
            return null;
        }
    }
}
