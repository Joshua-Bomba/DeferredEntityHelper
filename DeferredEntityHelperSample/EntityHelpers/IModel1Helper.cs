using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelperSample.EntityHelpers
{
    public interface IModel1Helper
    {

    }


    public partial class EntityHelper : IModel1Helper
    {
        public IModel1Helper Model1Helper => this;
    }
}
