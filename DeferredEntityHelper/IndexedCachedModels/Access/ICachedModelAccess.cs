using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.IndexedCachedModels
{
    public interface ICachedModelAccess<TKey,TValue> : IAsyncEnumerable<TValue> where TKey : notnull where TValue : class 
    {
        ValueTask<TValue?> AtKey(TKey key);
        ValueTask<IDictionary<TKey, TValue>> AsDictionary(TKey key);
    }
}
