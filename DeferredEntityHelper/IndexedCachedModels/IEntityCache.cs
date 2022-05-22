using DeferredEntityHelper.Futures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.IndexedCachedModels
{
    public interface IEntityCache
    {
        void Add(IFutureDetermined t);
    }
}
