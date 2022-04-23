using DeferredEntityHelper.DataBaseFutures;
using DeferredEntityHelperSample.EntityHelpers;
using DeferredEntityHelperSample.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelperSample
{
    [TestFixture]
    public class ContextTests
    {
        [Test]
        public void InitInMemoryContext()
        {
            using(SampleContext context = new SampleContext())
            {
                context.Model4s.Add(new Model4 { Id = 1, Type = "TestType" });
                context.Model1s.Add(new Model1 { Id = 1, Model4Id = 1, SomethingUnique = "SomeText"});
                context.Model3s.Add(new Model3 { Id = 1, IDKSomeThingElse = "SomeDatas" });
                context.Model2s.Add(new Model2 { Id = 1, Model1Id = 1, Model3Id = 1});


                context.SaveChanges();
                Model1[] d1= context.Model1s.ToArray();
                Model2[] d2= context.Model2s.ToArray();
                Model3[] d3= context.Model3s.ToArray();
                Model4[] d4= context.Model4s.ToArray();
            }
        }


        private async ValueTask TestEntityHelper()
        {
            using(SampleContext context = new SampleContext())
            {
                await using(EntityHelper eh = new EntityHelper(context))
                {
                    PotentialFuture<Model4> m4 = await eh.Model4Helper.CreateModel4IfItDoesNotExist("Test");
                    PotentialFuture<Model1> m1 = await eh.Model1Helper.CreateModel1("IDK Something Unique", m4);
                    PotentialFuture<Model3> m3 = await eh.Model3Helper.CreateModel2AndModel3("Idk SomethingElse", m1);
                }
            }
        }


        [Test]
        public void EntityHelperTest()
        {
            TestEntityHelper().GetAwaiter().GetResult();
        }
    }
}
