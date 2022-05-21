using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.DataBaseFutures
{
    public interface IFuture<T> : IFuture where T : class
    {
        new T GetItem();
    }

    public interface IFuture
    {
        object GetItem();
        bool Resolved { get; }
    }
}
