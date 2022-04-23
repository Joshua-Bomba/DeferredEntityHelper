using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelperSample.Models
{
    public class Model4
    { 
        public int Id { get; set; }

        public string Type { get; set; }

        public virtual List<Model1> Model1s { get; set; }
    }
}
