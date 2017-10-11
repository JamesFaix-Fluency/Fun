using System;

namespace Fun.Linq
{
    public static class TryExtensions
    {
        public static Result<T2> Select<T1, T2>(
            this Result<T1> @this,
            Func<T1, T2> projection) =>
            @this.Map(projection);

        public static Result<T3> SelectMany<T1, T2, T3>(
            this Result<T1> @this,
            Func<T1, Result<T2>> projection,
            Func<T1, T2, T3> resultSelector) =>
            @this.Map(t1 =>
                projection(t1).Map(t2 =>
                    resultSelector(t1, t2)));
    }
}
