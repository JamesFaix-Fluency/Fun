using System;

namespace Fun.Linq
{
    public static class TryExtensions
    {
        public static Try<T2> Select<T1, T2>(
            this Try<T1> @this,
            Func<T1, T2> projection) =>
            @this.Map(projection);

        public static Try<T2> SelectMany<T1, T2>(
            this Try<T1> @this,
            Func<T1, Try<T2>> projection) =>
            @this.Map(projection);
    }
}
