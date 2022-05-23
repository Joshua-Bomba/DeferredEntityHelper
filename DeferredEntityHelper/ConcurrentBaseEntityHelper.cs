using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper
{
    public abstract class ConcurrentBaseEntityHelper<T> : BaseEntityHelper<T> where T : DbContext
    {
        public ConcurrentBaseEntityHelper(T context) : base(context)
        {

        }
    }
}
