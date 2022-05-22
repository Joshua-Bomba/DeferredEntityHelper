using DeferredEntityHelper.DataBaseFutures;
using DeferredEntityHelper.DataBaseFutures.Callback;
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
            //Create Model 3
            Model3 m3 = new Model3
            {
                IDKSomeThingElse = idkSomethingElse
            };

            //We need this model to constructor other models
            //When SaveChanges is called it it will call the function ptr
            return await this.AddEntityAsync(m3, async x =>
            {
                //Once Model3 is resolve we will wait for the PotentailFuture passed in
                //it might be possible that one SaveChances has resolved it
                await this.WaitForPromises<Model2,Model1>(async m1 =>
                {                    
                    //Now we can create Model2 with the 2 diffrent id
                    Model2 m2 = new Model2
                    {
                        Model1Id = m1.Id,
                        Model3 = x,
                    };
                    return await this.AddEntityAsync(m2);//add the Model2 Entity and return it, guess we don't need to return it
                },pm1);                
            });
        }
    }
}
