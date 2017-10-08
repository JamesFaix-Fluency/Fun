using System;
using System.Threading.Tasks;

namespace Fun.Linq
{
    public static class TaskExtensions
    {
        public static async Task<T2> Select<T1, T2>(
            this Task<T1> @this,
            Func<T1, T2> projection)
        {
            return projection(await @this);
        }

        public static async Task<T3> SelectMany<T1, T2, T3>(
            this Task<T1> @this,
            Func<T1, Task<T2>> projection,
            Func<T1, T2, T3> resultSelector)
        {
            var t1 = await @this;
            var t2 = await projection(t1);
            return resultSelector(t1, t2);
        }
    }
}
