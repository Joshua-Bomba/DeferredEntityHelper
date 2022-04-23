using DeferredEntityHelper.DataBaseFutures;
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
        Task<PotentialFuture<Model4>> CreateModel4IfItDoesNotExist(string type);
    }

    public partial class EntityHelper : IModel4Helper
    {
        public IModel4Helper Model4Helper => this;

        async Task<PotentialFuture<Model4>> IModel4Helper.CreateModel4IfItDoesNotExist(string type)
        {
            Model4? m4 = await this._cacheManager.GetCachedIndexedDictionary<string,Model4>(x=>x.Type).AtKey(type);
            if(m4 == null)
            {
                m4 = new Model4
                {
                    Type = type
                };
                return await this.AddEntityAsync(m4);
            }
            else
            {
                return m4;
            }
        }
    }
}
