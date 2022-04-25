using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.IndexedCachedModels
{
    public interface ILoadFromDataBase
    {
        void Stack(ILoadFromDataBase d);
        ValueTask Process(DbContext context);
    }

    public class LoadFromDataBase<T> : IEntityCacheData<T>, ILoadFromDataBase where T : class
    {
        private T[] _data;
        private ILoadFromDataBase _stack;
        private TaskCompletionSource _taskCompletionSource;
        public LoadFromDataBase()
        {
            _data = null;
            _taskCompletionSource = new TaskCompletionSource();
        }

        public async ValueTask Finished() => await _taskCompletionSource.Task;

        public IEnumerable<T> GetData() => _data;

        public async ValueTask Process(DbContext context)
        {
            _data = await context.Set<T>().ToArrayAsync();
            _taskCompletionSource.SetResult();
            if(_stack != null)
            {
                await _stack.Process(context);
            }
        }

        public void Stack(ILoadFromDataBase d)
        {
            if(_stack != null)
            {
                _stack.Stack(d);
            }
        }
    }
}
