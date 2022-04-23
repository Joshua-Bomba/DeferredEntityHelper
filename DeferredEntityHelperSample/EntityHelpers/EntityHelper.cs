using DeferredEntityHelper;
using DeferredEntityHelperSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelperSample.EntityHelpers
{
    public class EntityHelper : BaseEntityHelper<SampleContext>
    {
        public EntityHelper() : base(new SampleContext())
        {

        }
    }
}
