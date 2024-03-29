﻿using DeferredEntityHelper.Futures;
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
        void Add(IFutureDetermined<T> entity);
        ValueTask SetupCacheSetFromDb(IBaseEntityHelper context);
        ValueTask Finished();
        void SetupCacheFromRelated(IEntityCacheIndexed<T> relatedSet);
        IEnumerable<IFutureDetermined<T>> GetData();
    }
}
