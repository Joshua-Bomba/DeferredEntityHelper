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
            //This WaitForPromises will wait till the PotentialFutures passed in the arguments list are resolved
            //in which case it will call the function pointer
            //we pass in the model4 potential future and will wait till that is ready
            //This will return a DatabaseFutureUnDetermined which does not have any information about the Model yet
            return await this.WaitForPromises<Model1, Model4>(async x =>
            {
                //this get results will call SaveChanges if the PotentialFuture is not Resolved
                //it should be resolved since we passed it into the WaitForPromise method

                //Now that Model4 is resolved we have the ID
                Model1 model1 = new Model1
                {
                    SomethingUnique = somethingUnique,
                    Model4Id = x.Id
                };
                //let's add the entity
                return await this.AddEntityAsync(model1);
            },model4);
        }
    }
}
