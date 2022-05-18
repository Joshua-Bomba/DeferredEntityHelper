using DeferredEntityHelper.DataBaseFutures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredEntityHelper
{
    //this is tedious but a bit nice to use
    //actually visual studio intellicode handled most of it
    public static class WaitForPromisesGenericList
    {
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1>(IFuture<TProp1> arg1) 
            where TResult : class where TProp1 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1,TProp2>(IFuture<TProp1> arg1,IFuture<TProp2> arg2) 
            where TResult : class where TProp1 : class where TProp2 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3>(IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4>(IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5>(IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6>(IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7>(IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6, IFuture<TProp7> arg7)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8>(IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6, IFuture<TProp7> arg7, IFuture<TProp8> arg8)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9>(IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6, IFuture<TProp7> arg7, IFuture<TProp8> arg8, IFuture<TProp9> arg9)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10>(IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6, IFuture<TProp7> arg7, IFuture<TProp8> arg8, IFuture<TProp9> arg9, IFuture<TProp10> arg10)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class where TProp10 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11>(IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6, IFuture<TProp7> arg7, IFuture<TProp8> arg8, IFuture<TProp9> arg9, IFuture<TProp10> arg10, IFuture<TProp11> arg11)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class where TProp10 : class where TProp11 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11, TProp12>(IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6, IFuture<TProp7> arg7, IFuture<TProp8> arg8, IFuture<TProp9> arg9, IFuture<TProp10> arg10, IFuture<TProp11> arg11, IFuture<TProp12> arg12)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class where TProp10 : class where TProp11 : class where TProp12 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11, TProp12, TProp13>(IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6, IFuture<TProp7> arg7, IFuture<TProp8> arg8, IFuture<TProp9> arg9, IFuture<TProp10> arg10, IFuture<TProp11> arg11, IFuture<TProp12> arg12, IFuture<TProp13> arg13)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class where TProp10 : class where TProp11 : class where TProp12 : class where TProp13 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11, TProp12, TProp13, TProp14>(IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6, IFuture<TProp7> arg7, IFuture<TProp8> arg8, IFuture<TProp9> arg9, IFuture<TProp10> arg10, IFuture<TProp11> arg11, IFuture<TProp12> arg12, IFuture<TProp13> arg13, IFuture<TProp14> arg14)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class where TProp10 : class where TProp11 : class where TProp12 : class where TProp13 : class where TProp14 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11, TProp12, TProp13, TProp14, TProp15>(IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6, IFuture<TProp7> arg7, IFuture<TProp8> arg8, IFuture<TProp9> arg9, IFuture<TProp10> arg10, IFuture<TProp11> arg11, IFuture<TProp12> arg12, IFuture<TProp13> arg13, IFuture<TProp14> arg14, IFuture<TProp15> arg15)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class where TProp10 : class where TProp11 : class where TProp12 : class where TProp13 : class where TProp14 : class where TProp15 : class;
        public delegate Task<PotentialFuture<TResult>> WaitForPromiseCallback<TResult, TProp1, TProp2, TProp3, TProp4, TProp5, TProp6, TProp7, TProp8, TProp9, TProp10, TProp11, TProp12, TProp13, TProp14, TProp15, TProp16>(IFuture<TProp1> arg1, IFuture<TProp2> arg2, IFuture<TProp3> arg3, IFuture<TProp4> arg4, IFuture<TProp5> arg5, IFuture<TProp6> arg6, IFuture<TProp7> arg7, IFuture<TProp8> arg8, IFuture<TProp9> arg9, IFuture<TProp10> arg10, IFuture<TProp11> arg11, IFuture<TProp12> arg12, IFuture<TProp13> arg13, IFuture<TProp14> arg14, IFuture<TProp15> arg15, IFuture<TProp16> arg16)
            where TResult : class where TProp1 : class where TProp2 : class where TProp3 : class where TProp4 : class where TProp5 : class where TProp6 : class where TProp7 : class where TProp8 : class where TProp9 : class where TProp10 : class where TProp11 : class where TProp12 : class where TProp13 : class where TProp14 : class where TProp15 : class where TProp16 : class;

        public static async Task<PotentialFuture<TResult>> WaitForPromise<TResult,TProp1>(this BaseEntityHelper entityHelper, WaitForPromiseCallback<TResult,TProp1> getResultFunction) where TResult : class where TProp1 : class
        {
            throw new NotImplementedException();
        }
    }
}
