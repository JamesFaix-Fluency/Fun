using System;

namespace Fun.Linq
{
    public static class MaybeExtensions
    {
        public static Maybe<T2> Select<T1, T2>(
            this Maybe<T1> @this,
            Func<T1, T2> projection) =>
            @this.Map(projection);

        public static Maybe<T2> SelectMany<T1, T2>(
            this Maybe<T1> @this,
            Func<T1, Maybe<T2>> projection) =>
            @this.Map(projection);
    }
}
