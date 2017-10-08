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


        private static void Test()
        {
            var a = from x in Opt.Some(4)
                    select x;
            
            var b = from x in Opt.Some(5)
                    from y in Opt.Some(7)
                    select x * y;

            var c = from x in Opt.Some(5)
                    from y in Opt.None<int>()
                    select x + y;
        }
    }
}
