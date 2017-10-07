using System;
using System.Collections.Generic;
using System.Linq;

namespace Fun
{
    public static class OptExtensions
    {
        #region Projections

        public static Opt<T2> OptMap<T1, T2>(
            this Opt<T1> @this,
            Func<T1, T2> projection) =>
            @this.Map1<T1, Unit, T2, Opt<T1>, Opt<T2>>(projection);

        public static Opt<T2> OptMap<T1, T2>(
            this Opt<T1> @this,
            Func<T1, Opt<T2>> projection) =>
            @this.Map1<T1, Unit, T2, Opt<T1>, Opt<T2>>(projection);

        #endregion

        #region Side effects

        public static Opt<T> OptDo<T>(
            this Opt<T> @this,
            Func<Unit> action) =>
            @this.Do1<T, Unit, Opt<T>>(action);

        public static Opt<T> OptDo<T>(
            this Opt<T> @this,
            Func<T, Unit> action) =>
            @this.Do1<T, Unit, Opt<T>>(action);

        public static Opt<T> OptDo<T>(
            this Opt<T> @this,
            Action action) =>
            @this.Do1<T, Unit, Opt<T>>(action);

        public static Opt<T> OptDo<T>(
            this Opt<T> @this,
            Action<T> action) =>
            @this.Do1<T, Unit, Opt<T>>(action);

        public static Opt<Unit> Ignore<T>(
            this Opt<T> @this)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return Opt.Some(Unit.Value);
        }

        #endregion

        #region Conversions

        public static Nullable<T> AsNullable<T>(
            this Opt<T> @this)
            where T : struct =>
            @this.HasValue
                ? @this.Value
                : default(T?);

        public static IEnumerable<T> AsSingleOrEmptySeq<T>(
            this Opt<T> @this)
            where T : struct =>
            @this.HasValue
                ? Enumerable.Repeat(@this.Value, 1)
                : Enumerable.Empty<T>();

        public static Try<T> AsTry<T>(
            this Opt<T> @this,
            Func<Exception> errorGenerator)
        {
            if (@this.HasValue)
            {
                return Try.Some(@this.Value);
            }
            else
            {
                try
                {
                    return Try.Error<T>(errorGenerator());
                }
                catch (Exception e)
                {
                    return Try.Error<T>(e);
                }
            }
        }

        #endregion
    }
}
