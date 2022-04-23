using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelperSample.EntityHelpers
{
    public interface IModel4Helper
    {

    }

    public partial class EntityHelper : IModel4Helper
    {
        public IModel4Helper Model4Helper => this;
    }
}
