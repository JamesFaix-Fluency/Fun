using System;
using System.Collections.Generic;
using System.Linq;

namespace Fun
{
    public static class MaybeExtensions
    {

        #region Projections

        public static Maybe<T2> Map<T1, T2>(
            this Maybe<T1> @this,
            Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            return @this.HasValue
                ? Maybe.Some(projection(@this.Value))
                : Maybe.None<T2>();
        }

        public static Maybe<T2> Map<T1, T2>(
            this Maybe<T1> @this,
            Func<T1, Maybe<T2>> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            return @this.HasValue
                ? projection(@this.Value)
                : Maybe.None<T2>();
        }

        #endregion

        #region Side effects

        public static Maybe<T> Do<T>(
            this Maybe<T> @this,
            Func<Unit> action)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.HasValue)
            {
                action();
            }

            return @this;
        }

        public static Maybe<T> Do<T>(
            this Maybe<T> @this,
            Func<T, Unit> action)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.HasValue)
            {
                action(@this.Value);
            }

            return @this;
        }

        public static Maybe<T> Do<T>(
            this Maybe<T> @this,
            Action action)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.HasValue)
            {
                action();
            }

            return @this;
        }

        public static Maybe<T> Do<T>(
            this Maybe<T> @this,
          Action<T> action)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.HasValue)
            {
                action(@this.Value);
            }

            return @this;
        }

        public static Maybe<Unit> Ignore<T>(
            this Maybe<T> @this)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return Maybe.Some(Unit.Value);
        }

        #endregion

        #region Conversions

        public static Nullable<T> AsNullable<T>(
            this Maybe<T> @this)
            where T : struct =>
            @this.HasValue
                ? @this.Value
                : default(T?);

        public static IEnumerable<T> AsSingleOrEmptySeq<T>(
            this Maybe<T> @this)
            where T : struct =>
            @this.HasValue
                ? Enumerable.Repeat(@this.Value, 1)
                : Enumerable.Empty<T>();

        public static Result<T> AsResult<T>(
            this Maybe<T> @this,
            Func<Exception> errorGenerator)
        {
            if (@this.HasValue)
            {
                return Result.Some(@this.Value);
            }
            else
            {
                try
                {
                    return Result.Error<T>(errorGenerator());
                }
                catch (Exception e)
                {
                    return Result.Error<T>(e);
                }
            }
        }

        #endregion
    }
}
