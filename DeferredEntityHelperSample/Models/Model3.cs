using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelperSample.Models
{
    public class Model3
    {
        public int Id { get; set; }

        public string? IDKSomeThingElse { get; set; }

        public List<Model2> Model2s { get; set; }
    }
}
