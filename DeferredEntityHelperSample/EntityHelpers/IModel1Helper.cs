using DeferredEntityHelper.DataBaseFutures;
using DeferredEntityHelperSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelperSample.EntityHelpers
{
    public interface IModel1Helper
    {
        Task<PotentialFuture<Model1>> CreateModel1(string somethingUnique, PotentialFuture<Model4> model4);
    }


    public partial class EntityHelper : IModel1Helper
    {
        public IModel1Helper Model1Helper => this;

        async Task<PotentialFuture<Model1>> IModel1Helper.CreateModel1(string somethingUnique, PotentialFuture<Model4> model4)
        {
            return await this.WaitForPromises<Model1>(async () =>
            {
                Model4 model4Real = await model4.GetResult();
                Model1 model1 = new Model1
                {
                    SomethingUnique = somethingUnique,
                    Model4 = model4Real
                };
                return await this.AddEntityAsync(model1);
            },model4);
        }
    }
}
