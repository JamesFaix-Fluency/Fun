using System;

namespace Fun.Linq
{
    public static class OptExtensions
    {
        public static Opt<T2> Select<T1, T2>(
            this Opt<T1> @this,
            Func<T1, T2> projection) =>
            @this.OptMap(projection);
        
        public static Opt<T3> SelectMany<T1, T2, T3>(
            this Opt<T1> @this,
            Func<T1, Opt<T2>> projection,
            Func<T1, T2, T3> resultSelector) =>
            @this.OptMap(t1 => 
                projection(t1).OptMap(t2 => 
                    resultSelector(t1, t2)));

        public static Opt<T> Where<T>(
            this Opt<T> @this,
            Func<T, bool> predicate) =>
            @this.HasValue
                ? predicate(@this.Value)
                    ? @this
                    : Opt.None<T>()
                : @this;
    }
}
