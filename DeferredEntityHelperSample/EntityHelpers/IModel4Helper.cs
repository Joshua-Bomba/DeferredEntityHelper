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
        Task<IFutureDetermined<Model4>> CreateModel4IfItDoesNotExist(string type, string secondValue);
    }

    public partial class EntityHelper : IModel4Helper
    {
        public IModel4Helper Model4Helper => this;

        async Task<IFutureDetermined<Model4>> IModel4Helper.CreateModel4IfItDoesNotExist(string type, string secondValue)
        {
            //First we check if the item is in the DataBase
            Model4? m4 = await this._cacheManager.GetCachedIndexedDictionary<string,Model4>(x=>x.Type).AtKey(type);
            if(m4 == null)
            {
                //if it does not exist we will create the Model
                m4 = new Model4
                {
                    Type = type,
                    ASecondValue = secondValue
                };

                //Adds it to the Context.
                //this will wrap it in a DatabaseFutureDetermined
                //this means we have access to the object via the GetUnresolvedItem() method
                //this means we could pass this into something else and it can modify it before SaveChanges is called
                return await this.AddEntityAsync(m4);

            }
            else
            {
                return m4;//If it exists we can return it and it will implicity wrap it in a PotentialFuture that is already resolved
            }
        }
    }
}
