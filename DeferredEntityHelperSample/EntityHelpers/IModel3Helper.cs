using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelperSample.EntityHelpers
{
    public interface IModel3Helper
    {

    }

    public partial class EntityHelper : IModel3Helper
    {
        public IModel3Helper Model3Helper => this;
    }
}
