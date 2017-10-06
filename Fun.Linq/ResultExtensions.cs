using System;

namespace Fun.Linq
{
    public static class ResultExtensions
    {
        public static Result<T2> Select<T1, T2>(
            this Result<T1> @this,
            Func<T1, T2> projection) =>
            @this.Map(projection);

        public static Result<T2> SelectMany<T1, T2>(
            this Result<T1> @this,
            Func<T1, Result<T2>> projection) =>
            @this.Map(projection);
    }
}
