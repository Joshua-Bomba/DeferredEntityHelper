using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.DataBaseFutures
{
    public interface IPostSaveOperations
    {
        Task TriggerFullSave();
        void AddUnresolvedElement(IDatabaseFuture f);
    }
}
