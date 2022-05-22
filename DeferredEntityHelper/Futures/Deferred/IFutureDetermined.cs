using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.Futures
{
    //don't think we can use an interface because of the implict cast stuff I would like to keep
    public interface IFutureDetermined : IFuture 
    {
        static IFutureDetermined<TProp> Wrap<TProp>(TProp el) where TProp : class => new FutureWrapper<TProp>(el);
    
    }
    public interface IFutureDetermined<T> : IFutureDetermined, IFuture<T> where T : class
    {

    }
}
