using System;
using System.Collections.Generic;
using System.Linq;

namespace Fun
{
    public static class NullableExtensions
    {
        #region Conversions

        public static Maybe<T> AsMaybe<T>(
            this Nullable<T> @this)
            where T : struct =>
            @this.HasValue
                ? Maybe.Some(@this.Value)
                : Maybe.None<T>();

        public static IEnumerable<T> AsSingleOrEmptySeq<T>(
            this Nullable<T> @this)
            where T : struct =>
            @this.HasValue
                ? Enumerable.Repeat<T>(@this.Value, 1)
                : Enumerable.Empty<T>();

        #endregion
    }
}