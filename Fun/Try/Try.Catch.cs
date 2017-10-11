using System;
using System.Reflection;

namespace Fun
{
    public static partial class Try
    {
        public static Try<T> Catch<T>(
            this Try<T> @this,
            Func<Exception, T> projection)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(projection, null))
                return Error<T>(new ArgumentNullException(nameof(projection)));

            return Get(() =>
                @this.HasValue
                    ? @this
                    : Some(projection(@this.Error)));
        }

        public static Try<T> Catch<T>(
            this Try<T> @this,
            Func<Exception, Try<T>> projection)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(projection, null))
                return Error<T>(new ArgumentNullException(nameof(projection)));

            return Get(() =>
                @this.HasValue
                    ? @this
                    : projection(@this.Error));
        }

        public static Try<T> Catch<T>(
            this Try<T> @this,
            Type exceptionType,
            Func<Exception, Try<T>> projection)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(exceptionType, null))
                return Error<T>(new ArgumentNullException(nameof(exceptionType)));

            if (Equals(projection, null))
                return Error<T>(new ArgumentNullException(nameof(projection)));

            if (!exceptionType.IsAssignableFrom(typeof(Exception)))
                return Error<T>(new ArgumentException($"Exception type must extend {nameof(System)}.{nameof(Exception)}.", nameof(exceptionType)));

            return Get(() =>
                !@this.HasValue
                && @this.Error.GetType().IsAssignableFrom(exceptionType)
                    ? projection(@this.Error)
                    : @this);
        }

        public static Try<T> Catch<T>(
            this Try<T> @this,
            Func<Exception, bool> errorPredicate,
            Func<Exception, Try<T>> projection)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(errorPredicate, null))
                return Error<T>(new ArgumentNullException(nameof(errorPredicate)));

            if (Equals(projection, null))
                return Error<T>(new ArgumentNullException(nameof(projection)));

            return Get(() =>
                !@this.HasValue
                && errorPredicate(@this.Error)
                    ? projection(@this.Error)
                    : @this);
        }
    }
}
