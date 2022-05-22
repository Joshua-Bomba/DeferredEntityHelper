using DeferredEntityHelper.Futures;
using DeferredEntityHelperSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeferredEntityHelper.IndexedCachedModels;

namespace DeferredEntityHelperSample.EntityHelpers
{
    public interface IModel4Helper
    {
        Task<PotentialFuture<Model4>> CreateModel4IfItDoesNotExist(string type, string secondValue);

        Task<PotentialFuture<Model1>> CreateModel4IfItDoesNotExistAndReturnModel1Attached(string type, string secondValue, string d);
    }

    public partial class EntityHelper : IModel4Helper
    {
        public IModel4Helper Model4Helper => this;




        async Task<PotentialFuture<Model4>> IModel4Helper.CreateModel4IfItDoesNotExist(string type, string secondValue)
        {
            //First we check if the item is in the DataBase
            IFutureDetermined<Model4>? m4 = await this._cacheManager.GetCachedIndexedDictionary<string,Model4>(x=>x.Type).AtKey(type);
            if(m4?.GetItem() == null)
            {
                //if it does not exist we will create the Model
                Model4 newM = new Model4
                {
                    Type = type,
                    ASecondValue = secondValue
                };

                //Adds it to the Context.
                //this will wrap it in a DatabaseFutureDetermined
                //this means we have access to the object via the GetUnresolvedItem() method
                //this means we could pass this into something else and it can modify it before SaveChanges is called
                return await this.AddEntityAsync(newM);

            }
            else
            {
                return m4.AsPotentialFuture();
            }
        }

        async Task<PotentialFuture<Model1>> IModel4Helper.CreateModel4IfItDoesNotExistAndReturnModel1Attached(string type, string secondValue, string d)
        {
            IFutureDetermined<Model4>? m4 = await this._cacheManager.GetCachedIndexedDictionary<string, Model4>(x => x.Type).AtKey(type);
            Model4 i = m4?.GetItem();
            if (i != null)
            {
                return await this.WaitForPromises<Model1,Model4>(async x =>
                {
                    //Once the Item is resolved we can grab the first item
                    return x.Model1s.First();
                }, m4);
            }
            else
            {
                Model4 model = new Model4
                {
                    Type = type,
                    ASecondValue = secondValue
                };
                FutureDetermined<Model4> m4Future = await this.AddEntityAsync(model);

                return await this.WaitForPromises<Model1,Model4>(async x =>{
                    Model1 newM = new Model1
                    {
                        SomethingUnique = d,
                        Model4 = x
                    };
                    return await this.AddEntityAsync(newM);
                },m4Future);


            }
        }
    }
}
