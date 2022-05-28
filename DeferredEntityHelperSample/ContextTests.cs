using DeferredEntityHelper.Futures;
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
        [Order(1)]
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
                    Model4 modifyM4 = m4.GetItem();
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

        private async ValueTask TestResolvingCacheData()
        {
            using (SampleContext context = new SampleContext())
            {
                await using (EntityHelper eh = new EntityHelper(context))
                {
                    string org = "The Original Once Should be added and a new one should not be added again";
                    PotentialFuture<Model1> a1 = await eh.Model4Helper.CreateModel4IfItDoesNotExistAndReturnModel1Attached("LetAddThisForModel1",org,"a value");
                    //this Potential future is not resolve and undefined aldo our Model4 is added to the cache the model 1 is not resolved yet

                    //when we call it again since we have the same item it will fetch the Model 4 but when it tries to get the Model4 item it can't since it's not yet been added
                    PotentialFuture<Model1> a2 = await eh.Model4Helper.CreateModel4IfItDoesNotExistAndReturnModel1Attached("LetAddThisForModel1", "a new record should not be inserted because it already exists","Another Value");

                }
            }
        }

        private async ValueTask TestCacheGetRelatedElementOutOfOrder()
        {
            using (SampleContext context = new SampleContext())
            {
                await using (EntityHelper eh = new EntityHelper(context))
                {
                    string org = "The Original Once Should be added and a new one should not be added again";
                    //Create Model4 but don't Create Model1 yet
                    PotentialFuture<Model4> a1 = await eh.Model4Helper.CreateModel4IfItDoesNotExist("TestCacheGetRelatedElement", org);
                    //Get Model1 from cache it will not be yet added but it should wait for it to be resolved
                    PotentialFuture<Model1> a2 = await eh.Model4Helper.CreateModel4IfItDoesNotExistAndReturnModel1Attached("TestCacheGetRelatedElement", "a new record should not be inserted because it already exists","Another Value");
                    //let's add Model1 now
                    PotentialFuture<Model1> t = await eh.WaitForPromises<Model1, Model4>(async x =>
                     {
                         Model1 m1 = new Model1
                         {
                             SomethingUnique = "It should be this one",
                             Model4Id = x.Id
                         };
                         return await eh.AddEntityAsync(m1);
                     },a1);

                }
            }
        }

        private async ValueTask TestCache()
        {
            using (SampleContext context = new SampleContext())
            {
                //Let's Added type 
                await using (EntityHelper eh = new EntityHelper(context))
                {
                    PotentialFuture<Model4> test = await eh.Model4Helper.CreateModel4IfItDoesNotExist("Test", "A Second Value");
                }
            }
            using (SampleContext context = new SampleContext())
            {
                //it'll save
                await using (EntityHelper eh = new EntityHelper(context))
                {
                    //Since it's already in our database it should fetch that one
                    PotentialFuture<Model4> test1 = await eh.Model4Helper.CreateModel4IfItDoesNotExist("Test", "A Second Value");

                    //Now that the cache is loaded it should be quicker
                    PotentialFuture<Model4> test2 = await eh.Model4Helper.CreateModel4IfItDoesNotExist("Test", "A Second Value");

                }
            }
        }


        private async ValueTask TestConcurreny()
        {
            using (SampleContext context = new SampleContext())
            {

                await using (EntityHelper eh = new EntityHelper(context))
                {
                    
                    //I don't want to await this cause I feel like I can accomplish more rather then waiting arround
                    Task<PotentialFuture<Model4>> test1 = eh.Model4Helper.CreateModel4IfItDoesNotExist("Test", "A Second Value");

                    Task<PotentialFuture<Model1>>[] multipleinsertsAtonce = new Task<PotentialFuture<Model1>>[1000];
                    //let's add another entity
                    Parallel.For(0, multipleinsertsAtonce.Length, i =>
                    {
                        multipleinsertsAtonce[i] = eh.Model1Helper.CreateModel1("i'm gonna insert all of these at the same time entity framework" + i);
                    });
                    await Task.WhenAll(multipleinsertsAtonce);

                }
            }
        }


        [Order(2)]
        [Test]
        public void EntityHelperTest() => TestEntityHelper().GetAwaiter().GetResult();
        [Order(3)]
        [Test]
        public void CacheTest() => TestCache().GetAwaiter().GetResult();
        [Order(4)]
        [Test]
        public void ResolvingCacheData() => TestResolvingCacheData().GetAwaiter().GetResult();
        [Order(5)]
        [Test]
        public void ConcurrencyTest() => TestConcurreny().GetAwaiter().GetResult();
        [Order(6)]
        [Test]
        public void CacheGetRelatedElementOutOfOrder() => TestCacheGetRelatedElementOutOfOrder().GetAwaiter().GetResult();


    }
}
