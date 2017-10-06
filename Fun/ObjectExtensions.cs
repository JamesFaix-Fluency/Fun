using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fun
{
    public static class ObjectExtensions
    {
        public static Result<T> AsResult<T>(
            this T @this) =>
            Result.Some(@this);

        public static Maybe<T> AsMaybe<T>(
            this T @this) =>
            Maybe.Some(@this);

        public static Task<T> AsTask<T>(
            this T @this) =>
            Task.FromResult(@this);

        public static IEnumerable<T> AsSingleSeq<T>(
            this T @this) =>
            Enumerable.Repeat(@this, 1);
    }
}
