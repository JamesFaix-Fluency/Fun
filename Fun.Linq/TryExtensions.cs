using System;

namespace Fun.Linq
{
    public static class TryExtensions
    {
        public static Try<T2> Select<T1, T2>(
            this Try<T1> @this,
            Func<T1, T2> projection) =>
            @this.TryMap(projection);

        public static Try<T3> SelectMany<T1, T2, T3>(
            this Try<T1> @this,
            Func<T1, Try<T2>> projection,
            Func<T1, T2, T3> resultSelector) =>
            @this.TryMap(t1 =>
                projection(t1).TryMap(t2 =>
                    resultSelector(t1, t2)));
        
        private static void Test()
        {
            var a = from x in Try.Some(4)
                    select x;

            var b = from x in Try.Some(5)
                    from y in Try.Some(7)
                    select x * y;

            var c = from x in Try.Some(5)
                    from y in Try.Error<int>(new Exception())
                    select x + y;
        }
    }
}
