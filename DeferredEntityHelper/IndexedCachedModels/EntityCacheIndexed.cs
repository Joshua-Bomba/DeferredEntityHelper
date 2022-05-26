﻿using DeferredEntityHelper.Futures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.IndexedCachedModels
{
    public class EntityCacheIndexed<TModel, TModelAccessKey> : Dictionary<TModelAccessKey, IFutureDetermined<TModel>>, IEntityCacheIndexed<TModel>, ICachedModelAccess<TModelAccessKey, TModel> where TModel : class where TModelAccessKey : notnull
    {
        private Func<TModel, TModelAccessKey> _keyGetter;
        private ValueTask _setupTask;
        public EntityCacheIndexed(Func<TModel, TModelAccessKey> keyGetter) : base()
        {
            _keyGetter = keyGetter;
        }

        private async ValueTask _SetupTask(IBaseEntityHelper context)
        {
            //hmm were going to have to grab the expression from the constructor and add a null check that way 
            IAsyncEnumerator<TModel> en = context.GetAllEntitiesOfType<TModel>();
            while (await en.MoveNextAsync())
            {
                
                    this[_keyGetter(en.Current)] = IFutureDetermined.Wrap(en.Current);
            }
        }

        public ValueTask SetupCacheSetFromDb(IBaseEntityHelper context)
        {
            _setupTask = _SetupTask(context);
            return _setupTask;
        }

        public void Add(IFutureDetermined<TModel> entity)
        {
            if(entity != null)
            {
                TModel? i = entity.GetItem();
                if (i != null)
                    this[_keyGetter(i)] = entity;
            }
        }

        private async ValueTask _SetupCacheFromRelated(IEntityCacheIndexed<TModel> relatedSet)
        {
            await relatedSet.Finished();
            foreach (IFutureDetermined<TModel> entity in relatedSet.GetData())
            {
                this[_keyGetter(entity.GetItem())] = entity;
            }
        }

        public void SetupCacheFromRelated(IEntityCacheIndexed<TModel> relatedSet)
            => _setupTask = _SetupCacheFromRelated(relatedSet);

        public IEnumerable<IFutureDetermined<TModel>> GetData() => this.Values;

        public async ValueTask Finished() => await _setupTask;
    }
}
