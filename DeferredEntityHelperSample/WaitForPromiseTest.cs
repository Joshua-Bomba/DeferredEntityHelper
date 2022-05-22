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
    public class WaitForPromiseTest
    {
        public class MockFuture : IFuture<string>
        {
            public string Item { get; set; }
            public bool Resolved { get; set; }

            public string GetItem() => Item;

            object IFuture.GetItem() => GetItem();
        }

        public async ValueTask NonGenericParamWaitForPromiseTestAsync()
        {
            using (SampleContext context = new SampleContext())
            {
                await using (EntityHelper eh = new EntityHelper(context))
                {

                    MockFuture mf = new MockFuture();

                    await eh.WaitForPromises<Model4>(async () => await AddTestModel(eh),mf);

                    mf.Item = "Resolve";
                    mf.Resolved = true;
                }
            }
        }

        public static void TestParam(params string[] resolvedFuture)
        {
            for(int i =0;i < resolvedFuture.Length; i++)
            {
                Assert.AreEqual("Resolve: " + i, resolvedFuture[i]);
            }
        }

        public static async Task<FutureDetermined<Model4>> AddTestModel(EntityHelper eh)
        {
            Model4 model = new Model4
            {
                Type = "TestType"
            };

            return await eh.AddEntityAsync(model);
        }

        public static async Task<PotentialFuture<string>> TestFuture(EntityHelper eh ,Queue<MockFuture> q)
        {
            switch(q.Count)
            {
                case 1:
                    return await eh.WaitForPromises<string, string>(async (x) =>
                    {
                        TestParam(x);
                        return "Resolved";
                    },q.Dequeue());
                case 2:
                    return await eh.WaitForPromises<string, string,string>(async (x,y) =>
                    {
                        TestParam(x,y);
                        return "Resolved";
                    }, q.Dequeue(),q.Dequeue());
                case 3:
                    return await eh.WaitForPromises<string, string, string,string>(async (x, y,z) =>
                    {
                        TestParam(x, y,z);
                        return "Resolved";
                    }, q.Dequeue(), q.Dequeue(),q.Dequeue());
                case 4:
                    return await eh.WaitForPromises<string, string, string, string,string>(async (x, y, z, a) =>
                    {
                        TestParam(x, y, z,a);
                        return "Resolved";
                    }, q.Dequeue(), q.Dequeue(), q.Dequeue(),q.Dequeue());
                case 5:
                    return await eh.WaitForPromises<string, string, string, string, string,string>(async (x, y, z, a,b) =>
                    {
                        TestParam(x, y, z, a,b);
                        return "Resolved";
                    }, q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(),q.Dequeue());
                case 6:
                    return await eh.WaitForPromises<string, string, string, string, string, string,string>(async (x, y, z, a, b,c) =>
                    {
                        TestParam(x, y, z, a, b,c);
                        return "Resolved";
                    }, q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue());
                case 7:
                    return await eh.WaitForPromises<string, string, string, string, string, string, string,string>(async (x, y, z, a, b, c,d) =>
                    {
                        TestParam(x, y, z, a, b, c,d);
                        return "Resolved";
                    }, q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(),q.Dequeue());
                case 8:
                    return await eh.WaitForPromises<string, string, string, string, string, string, string, string,string>(async (x, y, z, a, b, c, d,e) =>
                    {
                        TestParam(x, y, z, a, b, c, d,e);
                        return "Resolved";
                    }, q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(),q.Dequeue());
                case 9:
                    return await eh.WaitForPromises<string, string, string, string, string, string, string, string, string,string>(async (x, y, z, a, b, c, d, e,f) =>
                    {
                        TestParam(x, y, z, a, b, c, d, e,f);
                        return "Resolved";
                    }, q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(),q.Dequeue());
                case 10:
                    return await eh.WaitForPromises<string, string, string, string, string, string, string, string, string, string,string>(async (x, y, z, a, b, c, d, e, f,g) =>
                    {
                        TestParam(x, y, z, a, b, c, d, e, f,g);
                        return "Resolved";
                    }, q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(),q.Dequeue());
                case 11:
                    return await eh.WaitForPromises<string, string, string, string, string, string, string, string, string, string, string,string>(async (x, y, z, a, b, c, d, e, f, g,h) =>
                    {
                        TestParam(x, y, z, a, b, c, d, e, f, g,h);
                        return "Resolved";
                    }, q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(),q.Dequeue());
                case 12:
                    return await eh.WaitForPromises<string, string, string, string, string, string, string, string, string, string, string, string,string>(async (x, y, z, a, b, c, d, e, f, g, h,i) =>
                    {
                        TestParam(x, y, z, a, b, c, d, e, f, g, h,i);
                        return "Resolved";
                    }, q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(),q.Dequeue());
                case 13:
                    return await eh.WaitForPromises<string, string, string, string, string, string, string, string, string, string, string, string, string, string>(async (x, y, z, a, b, c, d, e, f, g, h, i,j) =>
                    {
                        TestParam(x, y, z, a, b, c, d, e, f, g, h, i,j);
                        return "Resolved";
                    }, q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(),q.Dequeue(), q.Dequeue());
                case 14:
                    return await eh.WaitForPromises<string, string, string, string, string, string, string, string, string, string, string, string, string, string,string>(async (x, y, z, a, b, c, d, e, f, g, h, i, j,k) =>
                    {
                        TestParam(x, y, z, a, b, c, d, e, f, g, h, i, j,k);
                        return "Resolved";
                    }, q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(),q.Dequeue());
                case 15:
                    return await eh.WaitForPromises<string, string, string, string, string, string, string, string, string, string, string, string, string, string, string,string>(async (x, y, z, a, b, c, d, e, f, g, h, i, j, k,l) =>
                    {
                        TestParam(x, y, z, a, b, c, d, e, f, g, h, i, j, k,l);
                        return "Resolved";
                    }, q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(),q.Dequeue());
                case 16:
                    return await eh.WaitForPromises<string, string, string, string, string, string, string, string, string, string, string, string, string, string, string, string,string>(async (x, y, z, a, b, c, d, e, f, g, h, i, j, k, l,m) =>
                    {
                        TestParam(x, y, z, a, b, c, d, e, f, g, h, i, j, k, l,m);
                        return "Resolved";
                    }, q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(), q.Dequeue(),q.Dequeue());
                default:
                    return null;
                    break;
            }
        }

        public async ValueTask AllParamWaitForPromiseTestAsync()
        {
            using (SampleContext context = new SampleContext())
            {
                await using (EntityHelper eh = new EntityHelper(context))
                {

                    MockFuture[] mf = new MockFuture[16];
                    for(int i = 0;i < mf.Length; i++)
                    {
                        mf[i] = new MockFuture();
                        Queue<MockFuture> q = new Queue<MockFuture>(mf.Take(i + 1));
                        await TestFuture(eh,q);
                    }

                    for (int i = 0; i < mf.Length; i++)
                    {
                        mf[i].Item = "Resolve: " + i;
                        mf[i].Resolved = true;
                    }
                }
            }
        }


        [Test]
        public void NonGenericParamWaitForPromiseTest() => NonGenericParamWaitForPromiseTestAsync().GetAwaiter().GetResult();
        [Test]
        public void AllParamWaitForPromiseTest() => AllParamWaitForPromiseTestAsync().GetAwaiter().GetResult();

    }
}
