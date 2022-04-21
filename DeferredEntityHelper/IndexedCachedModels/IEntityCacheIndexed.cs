using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.IndexedCachedModels
{
    public interface IEntityCacheIndexed<T> where T : class
    {
        void Add(T entity);
        ValueTask SetupCacheSetFromDb(DbContext context);
        ValueTask SetupCacheFromRelated(IEntityCacheIndexed<T> relatedSet);
        IAsyncEnumerable<T> GetData();
    }
}
