using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fun
{
    public static class ObjectExtensions
    {
        public static Try<T> AsTry<T>(
            this T @this) =>
            Try.Some(@this);

        public static Opt<T> AsOpt<T>(
            this T @this) =>
            Opt.Some(@this);

        public static Task<T> AsTask<T>(
            this T @this) =>
            Task.FromResult(@this);

        public static IEnumerable<T> AsSingleSeq<T>(
            this T @this) =>
            Enumerable.Repeat(@this, 1);
    }
}
