﻿using DeferredEntityHelper.DataBaseFutures;
using DeferredEntityHelper.DataBaseFutures.Callback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper
{
    //this is tedious but a bit nice to use
    //actually visual studio intellicode handled most of it
    public abstract partial class BaseEntityHelper
    {
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1>(TProp1 arg1)
            where TResult : class where TProp1 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2>(TProp1 arg1, TProp2 arg2)
            where TResult : class where TProp1 : class where TProp2 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3>(TProp1 arg1, TProp2 arg2, TProp3 arg3)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4>(TProp1 arg1, TProp2 arg2, TProp3 arg3, TProp4 arg4)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5>(TProp1 arg1, TProp2 arg2, TProp3 arg3, TProp4 arg4, TProp5 arg5)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6>(TProp1 arg1, TProp2 arg2, TProp3 arg3, TProp4 arg4, TProp5 arg5, TProp6 arg6)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7>(TProp1 arg1, TProp2 arg2, TProp3 arg3, TProp4 arg4, TProp5 arg5, TProp6 arg6, TProp7 arg7)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8>(TProp1 arg1, TProp2 arg2, TProp3 arg3, TProp4 arg4, TProp5 arg5, TProp6 arg6, TProp7 arg7, TProp8 arg8)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9>(TProp1 arg1, TProp2 arg2, TProp3 arg3, TProp4 arg4, TProp5 arg5, TProp6 arg6, TProp7 arg7, TProp8 arg8, TProp9 arg9)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10>(TProp1 arg1, TProp2 arg2, TProp3 arg3, TProp4 arg4, TProp5 arg5, TProp6 arg6, TProp7 arg7, TProp8 arg8, TProp9 arg9, TProp10 arg10)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class where TProp10 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11>(TProp1 arg1, TProp2 arg2, TProp3 arg3, TProp4 arg4, TProp5 arg5, TProp6 arg6, TProp7 arg7, TProp8 arg8, TProp9 arg9, TProp10 arg10, TProp11 arg11)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class where TProp10 : class where TProp11 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11, TProp12>(TProp1 arg1, TProp2 arg2, TProp3 arg3, TProp4 arg4, TProp5 arg5, TProp6 arg6, TProp7 arg7, TProp8 arg8, TProp9 arg9, TProp10 arg10, TProp11 arg11, TProp12 arg12)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class where TProp10 : class where TProp11 : class where TProp12 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11, TProp12, TProp13>(TProp1 arg1, TProp2 arg2, TProp3 arg3, TProp4 arg4, TProp5 arg5, TProp6 arg6, TProp7 arg7, TProp8 arg8, TProp9 arg9, TProp10 arg10, TProp11 arg11, TProp12 arg12, TProp13 arg13)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class where TProp10 : class where TProp11 : class where TProp12 : class where TProp13 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11, TProp12, TProp13, TProp14>(TProp1 arg1, TProp2 arg2, TProp3 arg3, TProp4 arg4, TProp5 arg5, TProp6 arg6, TProp7 arg7, TProp8 arg8, TProp9 arg9, TProp10 arg10, TProp11 arg11, TProp12 arg12, TProp13 arg13, TProp14 arg14)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class where TProp10 : class where TProp11 : class where TProp12 : class where TProp13 : class where TProp14 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11, TProp12, TProp13, TProp14, TProp15>(TProp1 arg1, TProp2 arg2, TProp3 arg3, TProp4 arg4, TProp5 arg5, TProp6 arg6, TProp7 arg7, TProp8 arg8, TProp9 arg9, TProp10 arg10, TProp11 arg11, TProp12 arg12, TProp13 arg13, TProp14 arg14, TProp15 arg15)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class where TProp10 : class where TProp11 : class where TProp12 : class where TProp13 : class where TProp14 : class where TProp15 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11, TProp12, TProp13, TProp14, TProp15, TProp16>(TProp1 arg1, TProp2 arg2, TProp3 arg3, TProp4 arg4, TProp5 arg5, TProp6 arg6, TProp7 arg7, TProp8 arg8, TProp9 arg9, TProp10 arg10, TProp11 arg11, TProp12 arg12, TProp13 arg13, TProp14 arg14, TProp15 arg15, TProp16 arg16)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class where TProp10 : class where TProp11 : class where TProp12 : class where TProp13 : class where TProp14 : class where TProp15 : class where TProp16 : class;

       
        public async Task<PotentialFuture<TResult>> WaitForPromises<TResult>(Func<Task<PotentialFuture<TResult>>> f, params IFuture[] wait) where TResult : class
            => await WaitForPromises<TResult>(new FutureCallbackHandler<TResult>(f, wait));

        public async Task<PotentialFuture<TResult>> WaitForPromises<TResult, TProp1>(WaitForPromiseCallback<TResult, TProp1> getResultFunction, IFuture<TProp1> arg1)
            where TResult : class where TProp1 : class => await WaitForPromises<TResult>(new FutureCallBackWithParams<TResult, TProp1>(getResultFunction).AttachItems(arg1));
        public async Task<PotentialFuture<TResult>> WaitForPromises<TResult, TProp1, TProp2>(WaitForPromiseCallback<TResult, TProp1, TProp2> getResultFunction, IFuture<TProp1> arg1, IFuture<TProp2> arg2)
            where TResult : class where TProp1 : class where TProp2 : class => throw new NotImplementedException();
        public async Task<PotentialFuture<TResult>> WaitForPromises<TResult, TProp1, TProp2, TProp3>(WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3> getResultFunction, IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class => throw new NotImplementedException();
        public async Task<PotentialFuture<TResult>> WaitForPromises<TResult, TProp1, TProp2, TProp3, TProp4>(WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4> getResultFunction, IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class => throw new NotImplementedException();
        public async Task<PotentialFuture<TResult>> WaitForPromises<TResult, TProp1, TProp2, TProp3, TProp4, TProp5>(WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5> getResultFunction, IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class => throw new NotImplementedException();
        public async Task<PotentialFuture<TResult>> WaitForPromises<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6>(WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6> getResultFunction, IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class => throw new NotImplementedException();
        public async Task<PotentialFuture<TResult>> WaitForPromises<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7>(WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7> getResultFunction, IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6, IFuture<TProp7> arg7)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class => throw new NotImplementedException();
        public async Task<PotentialFuture<TResult>> WaitForPromises<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8>(WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8> getResultFunction, IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6, IFuture<TProp7> arg7, IFuture<TProp8> arg8)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class => throw new NotImplementedException();
        public async Task<PotentialFuture<TResult>> WaitForPromises<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9>(WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9> getResultFunction, IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6, IFuture<TProp7> arg7, IFuture<TProp8> arg8, IFuture<TProp9> arg9)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class => throw new NotImplementedException();
        public async Task<PotentialFuture<TResult>> WaitForPromises<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10>(WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10> getResultFunction, IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6, IFuture<TProp7> arg7, IFuture<TProp8> arg8, IFuture<TProp9> arg9, IFuture<TProp10> arg10)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class where TProp10 : class => throw new NotImplementedException();
        public async Task<PotentialFuture<TResult>> WaitForPromises<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11>(WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11> getResultFunction, IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6, IFuture<TProp7> arg7, IFuture<TProp8> arg8, IFuture<TProp9> arg9, IFuture<TProp10> arg10, IFuture<TProp11> arg11)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class where TProp10 : class where TProp11 : class => throw new NotImplementedException();
        public async Task<PotentialFuture<TResult>> WaitForPromises<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11, TProp12>(WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11, TProp12> getResultFunction, IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6, IFuture<TProp7> arg7, IFuture<TProp8> arg8, IFuture<TProp9> arg9, IFuture<TProp10> arg10, IFuture<TProp11> arg11, IFuture<TProp12> arg12)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class where TProp10 : class where TProp11 : class where TProp12 : class => throw new NotImplementedException();
        public async Task<PotentialFuture<TResult>> WaitForPromises<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11, TProp12, TProp13>(WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11, TProp12, TProp13> getResultFunction, IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6, IFuture<TProp7> arg7, IFuture<TProp8> arg8, IFuture<TProp9> arg9, IFuture<TProp10> arg10, IFuture<TProp11> arg11, IFuture<TProp12> arg12, IFuture<TProp13> arg13)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class where TProp10 : class where TProp11 : class where TProp12 : class where TProp13 : class => throw new NotImplementedException();
        public async Task<PotentialFuture<TResult>> WaitForPromises<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11, TProp12, TProp13, TProp14>(WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11, TProp12, TProp13, TProp14> getResultFunction, IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6, IFuture<TProp7> arg7, IFuture<TProp8> arg8, IFuture<TProp9> arg9, IFuture<TProp10> arg10, IFuture<TProp11> arg11, IFuture<TProp12> arg12, IFuture<TProp13> arg13, IFuture<TProp14> arg14)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class where TProp10 : class where TProp11 : class where TProp12 : class where TProp13 : class where TProp14 : class => throw new NotImplementedException();
        public async Task<PotentialFuture<TResult>> WaitForPromises<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11, TProp12, TProp13, TProp14, TProp15>(WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11, TProp12, TProp13, TProp14, TProp15> getResultFunction, IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6, IFuture<TProp7> arg7, IFuture<TProp8> arg8, IFuture<TProp9> arg9, IFuture<TProp10> arg10, IFuture<TProp11> arg11, IFuture<TProp12> arg12, IFuture<TProp13> arg13, IFuture<TProp14> arg14, IFuture<TProp15> arg15)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class where TProp10 : class where TProp11 : class where TProp12 : class where TProp13 : class where TProp14 : class where TProp15 : class => throw new NotImplementedException();
        public async Task<PotentialFuture<TResult>> WaitForPromises<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11, TProp12, TProp13, TProp14, TProp15, TProp16>(WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11, TProp12, TProp13, TProp14, TProp15, TProp16> getResultFunction, IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6, IFuture<TProp7> arg7, IFuture<TProp8> arg8, IFuture<TProp9> arg9, IFuture<TProp10> arg10, IFuture<TProp11> arg11, IFuture<TProp12> arg12, IFuture<TProp13> arg13, IFuture<TProp14> arg14, IFuture<TProp15> arg15, IFuture<TProp16> arg16)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class where TProp10 : class where TProp11 : class where TProp12 : class where TProp13 : class where TProp14 : class where TProp15 : class where TProp16 : class => throw new NotImplementedException();

        #region Adapters
        private class FutureCallBackWithParams<R, P1> : BaseFutureCallBackWithParams<R, WaitForPromiseCallback<R, P1>> where R : class where P1 : class { public FutureCallBackWithParams(WaitForPromiseCallback<R, P1> cb) : base(cb) { }
            public async override Task<PotentialFuture<R>> Callback() => await _cb(GetItem<P1>());
        }
        private class FutureCallBackWithParams<R, P1,P2> : BaseFutureCallBackWithParams<R, WaitForPromiseCallback<R, P1, P2>> where R : class where P1 : class where P2: class{ public FutureCallBackWithParams(WaitForPromiseCallback<R, P1, P2> cb) : base(cb) { }
            public async override Task<PotentialFuture<R>> Callback() => await _cb(GetItem<P1>(),GetItem<P2>());
        }
        private class FutureCallBackWithParams<R, P1, P2,P3> : BaseFutureCallBackWithParams<R, WaitForPromiseCallback<R, P1, P2,P3>> where R : class where P1 : class where P2 : class where P3: class { public FutureCallBackWithParams(WaitForPromiseCallback<R, P1, P2,P3> cb) : base(cb) { }
            public async override Task<PotentialFuture<R>> Callback() => await _cb(GetItem<P1>(), GetItem<P2>(),GetItem<P3>());
        }
		private class FutureCallBackWithParams<R, P1, P2,P3,P4> : BaseFutureCallBackWithParams<R, WaitForPromiseCallback<R, P1, P2,P3,P4>> where R : class where P1 : class where P2 : class where P3: class where P4 : class
        { public FutureCallBackWithParams(WaitForPromiseCallback<R, P1, P2,P3,P4> cb) : base(cb) { }
            public async override Task<PotentialFuture<R>> Callback() => await _cb(GetItem<P1>(), GetItem<P2>(),GetItem<P3>(),GetItem<P4>());
        }
		private class FutureCallBackWithParams<R, P1, P2,P3,P4,P5> : BaseFutureCallBackWithParams<R, WaitForPromiseCallback<R, P1, P2,P3,P4,P5>> where R : class where P1 : class where P2 : class where P3: class where P4 : class where P5 : class
        { public FutureCallBackWithParams(WaitForPromiseCallback<R, P1, P2,P3,P4,P5> cb) : base(cb) { }
            public async override Task<PotentialFuture<R>> Callback() => await _cb(GetItem<P1>(), GetItem<P2>(),GetItem<P3>(),GetItem<P4>(),GetItem<P5>());
        }
		private class FutureCallBackWithParams<R, P1, P2,P3,P4,P5,P6> : BaseFutureCallBackWithParams<R, WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6>> where R : class where P1 : class where P2 : class where P3: class where P4 : class where P5 : class where P6 : class
        { public FutureCallBackWithParams(WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6> cb) : base(cb) { }
            public async override Task<PotentialFuture<R>> Callback() => await _cb(GetItem<P1>(), GetItem<P2>(),GetItem<P3>(),GetItem<P4>(),GetItem<P5>(),GetItem<P6>());
        }
		private class FutureCallBackWithParams<R, P1, P2,P3,P4,P5,P6,P7> : BaseFutureCallBackWithParams<R, WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6,P7>> where R : class where P1 : class where P2 : class where P3: class where P4 : class where P5 : class where P6 : class where P7 : class
        { public FutureCallBackWithParams(WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6,P7> cb) : base(cb) { }
            public async override Task<PotentialFuture<R>> Callback() => await _cb(GetItem<P1>(), GetItem<P2>(),GetItem<P3>(),GetItem<P4>(),GetItem<P5>(),GetItem<P6>(),GetItem<P7>());
        }
		private class FutureCallBackWithParams<R, P1, P2,P3,P4,P5,P6,P7,P8> : BaseFutureCallBackWithParams<R, WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6,P7,P8>> where R : class where P1 : class where P2 : class where P3: class where P4 : class where P5 : class where P6 : class where P7 : class where P8 : class
        { public FutureCallBackWithParams(WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6,P7,P8> cb) : base(cb) { }
            public async override Task<PotentialFuture<R>> Callback() => await _cb(GetItem<P1>(), GetItem<P2>(),GetItem<P3>(),GetItem<P4>(),GetItem<P5>(),GetItem<P6>(),GetItem<P7>(),GetItem<P8>());
        }
		private class FutureCallBackWithParams<R, P1, P2,P3,P4,P5,P6,P7,P8,P9> : BaseFutureCallBackWithParams<R, WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6,P7,P8,P9>> where R : class where P1 : class where P2 : class where P3: class where P4 : class where P5 : class where P6 : class where P7 : class where P8 : class where P9 : class
        { public FutureCallBackWithParams(WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6,P7,P8,P9> cb) : base(cb) { }
            public async override Task<PotentialFuture<R>> Callback() => await _cb(GetItem<P1>(), GetItem<P2>(),GetItem<P3>(),GetItem<P4>(),GetItem<P5>(),GetItem<P6>(),GetItem<P7>(),GetItem<P8>(),GetItem<P9>());
        }
		private class FutureCallBackWithParams<R, P1, P2,P3,P4,P5,P6,P7,P8,P9,P10> : BaseFutureCallBackWithParams<R, WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6,P7,P8,P9,P10>> where R : class where P1 : class where P2 : class where P3: class where P4 : class where P5 : class where P6 : class where P7 : class where P8 : class where P9 : class where P10 : class
        { public FutureCallBackWithParams(WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6,P7,P8,P9,P10> cb) : base(cb) { }
            public async override Task<PotentialFuture<R>> Callback() => await _cb(GetItem<P1>(), GetItem<P2>(),GetItem<P3>(),GetItem<P4>(),GetItem<P5>(),GetItem<P6>(),GetItem<P7>(),GetItem<P8>(),GetItem<P9>(),GetItem<P10>());
        }
		private class FutureCallBackWithParams<R, P1, P2,P3,P4,P5,P6,P7,P8,P9,P10,P11> : BaseFutureCallBackWithParams<R, WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6,P7,P8,P9,P10,P11>> where R : class where P1 : class where P2 : class where P3: class where P4 : class where P5 : class where P6 : class where P7 : class where P8 : class where P9 : class where P10 : class where P11 : class
        { public FutureCallBackWithParams(WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6,P7,P8,P9,P10,P11> cb) : base(cb) { }
            public async override Task<PotentialFuture<R>> Callback() => await _cb(GetItem<P1>(), GetItem<P2>(),GetItem<P3>(),GetItem<P4>(),GetItem<P5>(),GetItem<P6>(),GetItem<P7>(),GetItem<P8>(),GetItem<P9>(),GetItem<P10>(),GetItem<P11>());
        }
		private class FutureCallBackWithParams<R, P1, P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12> : BaseFutureCallBackWithParams<R, WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12>> where R : class where P1 : class where P2 : class where P3: class where P4 : class where P5 : class where P6 : class where P7 : class where P8 : class where P9 : class where P10 : class where P11 : class where P12 : class
        { public FutureCallBackWithParams(WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12> cb) : base(cb) { }
            public async override Task<PotentialFuture<R>> Callback() => await _cb(GetItem<P1>(), GetItem<P2>(),GetItem<P3>(),GetItem<P4>(),GetItem<P5>(),GetItem<P6>(),GetItem<P7>(),GetItem<P8>(),GetItem<P9>(),GetItem<P10>(),GetItem<P11>(),GetItem<P12>());
        }
		private class FutureCallBackWithParams<R, P1, P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13> : BaseFutureCallBackWithParams<R, WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13>> where R : class where P1 : class where P2 : class where P3: class where P4 : class where P5 : class where P6 : class where P7 : class where P8 : class where P9 : class where P10 : class where P11 : class where P12 : class where P13 : class
        { public FutureCallBackWithParams(WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13> cb) : base(cb) { }
            public async override Task<PotentialFuture<R>> Callback() => await _cb(GetItem<P1>(), GetItem<P2>(),GetItem<P3>(),GetItem<P4>(),GetItem<P5>(),GetItem<P6>(),GetItem<P7>(),GetItem<P8>(),GetItem<P9>(),GetItem<P10>(),GetItem<P11>(),GetItem<P12>(),GetItem<P13>());
        }
		private class FutureCallBackWithParams<R, P1, P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13,P14> : BaseFutureCallBackWithParams<R, WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13,P14>> where R : class where P1 : class where P2 : class where P3: class where P4 : class where P5 : class where P6 : class where P7 : class where P8 : class where P9 : class where P10 : class where P11 : class where P12 : class where P13 : class where P14 : class
        { public FutureCallBackWithParams(WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13,P14> cb) : base(cb) { }
            public async override Task<PotentialFuture<R>> Callback() => await _cb(GetItem<P1>(), GetItem<P2>(),GetItem<P3>(),GetItem<P4>(),GetItem<P5>(),GetItem<P6>(),GetItem<P7>(),GetItem<P8>(),GetItem<P9>(),GetItem<P10>(),GetItem<P11>(),GetItem<P12>(),GetItem<P13>(), GetItem<P14>());
        }
		private class FutureCallBackWithParams<R, P1, P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13,P14,P15> : BaseFutureCallBackWithParams<R, WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13,P14,P15>> where R : class where P1 : class where P2 : class where P3: class where P4 : class where P5 : class where P6 : class where P7 : class where P8 : class where P9 : class where P10 : class where P11 : class where P12 : class where P13 : class where P14 : class where P15 : class
        { public FutureCallBackWithParams(WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13,P14,P15> cb) : base(cb) { }
            public async override Task<PotentialFuture<R>> Callback() => await _cb(GetItem<P1>(), GetItem<P2>(),GetItem<P3>(),GetItem<P4>(),GetItem<P5>(),GetItem<P6>(),GetItem<P7>(),GetItem<P8>(),GetItem<P9>(),GetItem<P10>(),GetItem<P11>(),GetItem<P12>(),GetItem<P13>(), GetItem<P14>(),GetItem<P15>());
        }
		private class FutureCallBackWithParams<R, P1, P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13,P14,P15,P16> : BaseFutureCallBackWithParams<R, WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13,P14,P15,P16>> where R : class where P1 : class where P2 : class where P3: class where P4 : class where P5: class where P6 : class where P7 : class where P8  : class where P9 : class where P10 : class where P11 : class where P12 : class where P13 : class where P14 : class where P15 : class where P16 : class{ public FutureCallBackWithParams(WaitForPromiseCallback<R, P1, P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13,P14,P15,P16> cb) : base(cb) { }
            public async override Task<PotentialFuture<R>> Callback() => await _cb(GetItem<P1>(), GetItem<P2>(),GetItem<P3>(),GetItem<P4>(),GetItem<P5>(),GetItem<P6>(),GetItem<P7>(),GetItem<P8>(),GetItem<P9>(),GetItem<P10>(),GetItem<P11>(),GetItem<P12>(),GetItem<P13>(), GetItem<P14>(), GetItem<P15>(),GetItem<P16>());
        }
		

        #endregion


    }
}
