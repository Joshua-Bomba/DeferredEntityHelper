using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelperSample.Models
{
    public class Model2
    {
        public int Id { get; set; }

        public int? Model1Id { get; set; }

        [ForeignKey("Model1Id")]
        public Model1 Model1 { get; set; }

        public int? Model3Id { get; set; }
        [ForeignKey("Model3Id")]
        public Model3 Model3 { get; set; }
    }
}
