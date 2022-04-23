using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelperSample.EntityHelpers
{
    public interface IModel2Helper
    {

    }

    public partial class EntityHelper : IModel2Helper
    {
        public IModel2Helper Model2Helper => this;
    }
}
