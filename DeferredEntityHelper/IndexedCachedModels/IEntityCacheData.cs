using DeferredEntityHelper.DataBaseFutures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.IndexedCachedModels
{
    public interface IEntityCacheData<T> where T : class
    {
        ValueTask Finished();
        IEnumerable<PotentialFuture<T>> GetData();
    }
}
