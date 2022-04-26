using DeferredEntityHelper.DataBaseFutures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.IndexedCachedModels
{
    public interface IEntityCache : IFuture
    {
        void Add(object t);
    }
}
