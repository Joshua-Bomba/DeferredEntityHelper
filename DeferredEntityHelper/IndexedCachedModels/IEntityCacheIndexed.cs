using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.IndexedCachedModels
{
    public interface IEntityCacheIndexed<T> : IEntityCacheData<T> where T : class
    {
        void Add(T entity);
        void SetupCache(IEntityCacheData<T> set);
    }
}
