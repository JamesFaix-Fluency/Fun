using System;
using System.Collections.Generic;
using System.Linq;

namespace Fun.Extensions
{
    public static class NullableExtensions
    {
        #region Conversions

        public static Opt<T> AsOpt<T>(
            this Nullable<T> @this)
            where T : struct =>
            @this.HasValue
                ? Opt.Some(@this.Value)
                : Opt.None<T>();

        public static IEnumerable<T> AsSingleOrEmptySeq<T>(
            this Nullable<T> @this)
            where T : struct =>
            @this.HasValue
                ? Enumerable.Repeat<T>(@this.Value, 1)
                : Enumerable.Empty<T>();

        #endregion
    }
}