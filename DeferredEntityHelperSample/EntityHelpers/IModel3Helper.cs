using DeferredEntityHelper.DataBaseFutures;
using DeferredEntityHelperSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelperSample.EntityHelpers
{
    public interface IModel3Helper
    {
        Task<PotentialFuture<Model3>> CreateModel2AndModel3(string idkSomethingElse, PotentialFuture<Model1> pm1);
    }

    public partial class EntityHelper : IModel3Helper
    {
        public IModel3Helper Model3Helper => this;

        async Task<PotentialFuture<Model3>> IModel3Helper.CreateModel2AndModel3(string idkSomethingElse, PotentialFuture<Model1> pm1)
        {
            Model3 m3 = new Model3
            {
                IDKSomeThingElse = idkSomethingElse
            };

            return await this.AddEntityAsync(m3, async x =>
            {
                await this.WaitForPromises<Model2>(async () =>
                {
                    Model1 m1 = await pm1.GetResult();
                    Model2 m2 = new Model2
                    {
                        Model1 = m1,
                        Model3 = x,
                    };
                    return await this.AddEntityAsync(m2);
                },pm1);                
            });
        }
    }
}
