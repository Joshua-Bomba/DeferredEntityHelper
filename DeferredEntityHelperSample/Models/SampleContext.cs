using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelperSample.Models
{
    public class SampleContext : DbContext
    {

        public SampleContext() : base()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("TestDatabase");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Model1> Model1s { get; set; }

        public DbSet<Model2> Model2s { get; set; }
                           //hehe
        public DbSet<Model3> Model3s { get; set; }

        public DbSet<Model4> Model4s { get; set; }
    }
}
