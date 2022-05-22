using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures
{
    public interface IFutureDetermined : IFuture { }
    public interface IFutureDetermined<T> : IFutureDetermined, IFuture<T> where T : class
    {

    }
}
