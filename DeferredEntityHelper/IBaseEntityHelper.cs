using DeferredEntityHelper.Futures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper
{
    public interface IBaseEntityHelper
    {
        Task<FutureDetermined<TProp>> AddEntityAsync<TProp>(TProp e, Func<TProp, Task> actionPostSave = null) where TProp : class;

        IAsyncEnumerator<TProp> GetAllEntitiesOfType<TProp>(Func<IQueryable<TProp>, IQueryable<TProp>>? f = null) where TProp : class;
    }
}
