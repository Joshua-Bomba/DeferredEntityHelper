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
        [Order (1)]
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
                    //returns a DatabaseFutureDetermined
                    PotentialFuture<Model4> m4 = await eh.Model4Helper.CreateModel4IfItDoesNotExist("Test","valueThatWillBeModified");

                    //with a DatabaseFutureDetermined you can call GetUnresolvedItem and modify stuff. the ID will not be generated yet but you can still modifiy stuff if it's
                    //not saved
                    Model4 modifyM4 = m4.GetCurrentItem();
                    modifyM4.ASecondValue = "I'm Gonna Change This Before We save";

                    //returns a DatabaseFutureUnDetermined
                    PotentialFuture<Model1> m1 = await eh.Model1Helper.CreateModel1("IDK Something Unique", m4);
                    //The DatabaseFutureUnDetermined does not have any information about the Model1
                    //it's not created yet
                    //the GetUnresolvedItem() will return null

                    PotentialFuture<Model3> m3 = await eh.Model3Helper.CreateModel2AndModel3("Idk SomethingElse", m1);
                }
            }
        }

        private async ValueTask TestCache()
        {
            using(SampleContext context = new SampleContext())
            {
                //Let's Added type 
                await using(EntityHelper eh = new EntityHelper(context))
                {
                    PotentialFuture<Model4> test = await eh.Model4Helper.CreateModel4IfItDoesNotExist("Test", "A Second Value");
                }
                //it'll save
                await using(EntityHelper eh = new EntityHelper(context))
                {
                    //Since it's already in our database it should fetch that one
                    PotentialFuture<Model4> test1 = await eh.Model4Helper.CreateModel4IfItDoesNotExist("Test", "A Second Value");

                    //Now that the cache is loaded it should be quicker
                    PotentialFuture<Model4> test2 = await eh.Model4Helper.CreateModel4IfItDoesNotExist("Test", "A Second Value");
                }


            }
        }

        [Order(2)]
        [Test]
        public void EntityHelperTest() => TestEntityHelper().GetAwaiter().GetResult();
        [Order(3)]
        [Test]
        public void CacheTest() => TestCache().GetAwaiter().GetResult();


    }
}
