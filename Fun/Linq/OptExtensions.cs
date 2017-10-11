using System;

namespace Fun.Linq
{
    public static class OptExtensions
    {
        public static opt<T2> Select<T1, T2>(
            this opt<T1> @this,
            Func<T1, T2> projection) =>
            @this.Map(projection);
        
        public static opt<T3> SelectMany<T1, T2, T3>(
            this opt<T1> @this,
            Func<T1, opt<T2>> projection,
            Func<T1, T2, T3> resultSelector) =>
            @this.Map(t1 => 
                projection(t1).Map(t2 => 
                    resultSelector(t1, t2)));

        public static opt<T> Where<T>(
            this opt<T> @this,
            Func<T, bool> predicate) =>
            @this.HasValue
                ? predicate(@this.Value)
                    ? @this
                    : Opt.None<T>()
                : @this;        
    }
}
