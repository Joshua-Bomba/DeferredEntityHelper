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
        public static async ValueTask<TValue?> AtKey<TKey,TValue>(this ValueTask<ICachedModelAccess<TKey, TValue>> a, TKey key) where TKey : notnull where TValue : class
        {
            ICachedModelAccess<TKey, TValue> c = await a;
            return await c.AtKey(key);
        }
    }
}
