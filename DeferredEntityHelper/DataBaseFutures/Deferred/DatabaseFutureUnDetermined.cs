using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper.DataBaseFutures
{
    public class DatabaseFutureUnDetermined<T> : DatabaseFuture<T> where T : class
    {
        private IDatabaseFuture[] _wait;
        private Func<Task<PotentialFuture<T>>> _func;
        private PotentialFuture<T> _next;
        public DatabaseFutureUnDetermined(IDatabaseFuture[] wait, Func<Task<PotentialFuture<T>>> f, IPostSaveOperations efHelper) : base(null, efHelper)
        {
            _wait = wait;
            _func = f;
            _next = null;
        }

        public override void SavedChangesTriggered()
        {

        }

        public async override Task Process()
        {
            if (_next != null)
            {
                if (_next.Resolved)
                {
                    _data = await _next.GetResult();
                }
                else
                {
                    this._efHelper.AddUnresolvedElement(this);
                }
            }
            else if (_wait.Any(x => !x.Resolved))
            {
                this._efHelper.AddUnresolvedElement(this);//we will have it go around again till the promises we are waiting on are resolved
            }
            else
            {
                PotentialFuture<T> p = await _func();
                _wait = null;
                //save.Set(p);
                if (!p.Resolved)
                {
                    _next = p;
                    this._efHelper.AddUnresolvedElement(this);
                }
                else
                {
                    _data = await p.GetResult();
                    _resolved = true;
                }
            }
        }
    }
}
