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
            Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            return @this.Map1<T1, T2, Unit, Opt<T1>, Opt<T2>>(projection);
        }

        public static Opt<T2> OptMap<T1, T2>(
            this Opt<T1> @this,
            Func<T1, Opt<T2>> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            return @this.Map1<T1, T2, Unit, Opt<T1>, Opt<T2>>(projection);
        }

        #endregion

        #region Side effects

        public static Opt<T> OptDo<T>(
            this Opt<T> @this,
            Func<Unit> action)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            return @this.Do1<T, Unit, Opt<T>>(action);
        }

        public static Opt<T> OptDo<T>(
            this Opt<T> @this,
            Func<T, Unit> action)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            return @this.Do1<T, Unit, Opt<T>>(action);
        }

        public static Opt<T> OptDo<T>(
            this Opt<T> @this,
            Action action)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            return @this.Do1<T, Unit, Opt<T>>(action);
        }

        public static Opt<T> OptDo<T>(
            this Opt<T> @this,
            Action<T> action)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            return @this.Do1<T, Unit, Opt<T>>(action);
        }

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
            where T : struct
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return @this.HasValue
                ? @this.Value
                : default(T?);
        }

        public static IEnumerable<T> AsSingleOrEmptySeq<T>(
            this Opt<T> @this)
            where T : struct
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return @this.HasValue
                ? Enumerable.Repeat(@this.Value, 1)
                : Enumerable.Empty<T>();
        }

        public static Try<T> AsTry<T>(
            this Opt<T> @this,
            Func<Exception> errorGenerator)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(errorGenerator, null))
                throw new ArgumentNullException(nameof(errorGenerator));

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
