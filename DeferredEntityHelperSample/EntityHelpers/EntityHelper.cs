using DeferredEntityHelper;
using DeferredEntityHelperSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelperSample.EntityHelpers
{
    public partial class EntityHelper : BaseEntityHelper<SampleContext>
    {
        public EntityHelper(SampleContext context) : base(context) { }
    }
}
