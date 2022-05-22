using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures
{
    public interface IFutureEvent : IFuture
    {
        Task Process();
        void DependencyResolvedTrigger();
    }
}
