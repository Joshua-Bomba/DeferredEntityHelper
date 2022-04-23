using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelperSample.Models
{
    public class Model1
    {
        public int Id { get; set; }

        public int Model4Id { get; set; }

        [ForeignKey("Model4Id")]
        public Model4 Model4 { get; set; }

        public string? SomethingUnique { get; set; }

        public virtual List<Model2> Model2s { get; set; }


    }
}
