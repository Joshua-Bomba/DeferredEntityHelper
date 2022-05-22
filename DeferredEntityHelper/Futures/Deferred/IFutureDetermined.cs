using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures
{
    public interface IFutureDetermined : IFuture 
    {
        static IFutureDetermined<TProp> Wrap<TProp>(TProp el) where TProp : class => new FutureWrapper<TProp>(el);
    
    }
    public interface IFutureDetermined<T> : IFutureDetermined, IFuture<T> where T : class
    {

    }
}
