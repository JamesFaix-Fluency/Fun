using System;

namespace Fun.Linq
{
    public static class OptExtensions
    {
        public static Opt<T2> Select<T1, T2>(
            this Opt<T1> @this,
            Func<T1, T2> projection) =>
            @this.OptMap(projection);

        public static Opt<T2> SelectMany<T1, T2>(
            this Opt<T1> @this,
            Func<T1, Opt<T2>> projection) =>
            @this.OptMap(projection);
    }
}
