using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.DataBaseFutures
{
    public interface IDatabaseFuture : IFuture
    {
        Task Process();
        void SavedChangesTriggered();
    }
}
