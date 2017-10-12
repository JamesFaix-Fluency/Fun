using System;
using System.Reflection;

namespace Fun
{
    public static partial class Result
    {
        public static Result<T> Catch<T>(
            this Result<T> @this,
            Func<Exception, T> projection)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(projection, null))
                return Error<T>(new ArgumentNullException(nameof(projection)));

            return Try(() =>
                @this.HasValue
                    ? @this
                    : Value(projection(@this.Error)));
        }

        public static Result<T> Catch<T>(
            this Result<T> @this,
            Func<Exception, Result<T>> projection)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(projection, null))
                return Error<T>(new ArgumentNullException(nameof(projection)));

            return Try(() =>
                @this.HasValue
                    ? @this
                    : projection(@this.Error));
        }

        public static Result<T> Catch<T>(
            this Result<T> @this,
            Type exceptionType,
            Func<Exception, Result<T>> projection)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(exceptionType, null))
                return Error<T>(new ArgumentNullException(nameof(exceptionType)));

            if (Equals(projection, null))
                return Error<T>(new ArgumentNullException(nameof(projection)));

            if (!exceptionType.IsAssignableFrom(typeof(Exception)))
                return Error<T>(new ArgumentException($"Exception type must extend {nameof(System)}.{nameof(Exception)}.", nameof(exceptionType)));

            return Try(() =>
                !@this.HasValue
                && @this.Error.GetType().IsAssignableFrom(exceptionType)
                    ? projection(@this.Error)
                    : @this);
        }

        public static Result<T> Catch<T>(
            this Result<T> @this,
            Func<Exception, bool> errorPredicate,
            Func<Exception, Result<T>> projection)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(errorPredicate, null))
                return Error<T>(new ArgumentNullException(nameof(errorPredicate)));

            if (Equals(projection, null))
                return Error<T>(new ArgumentNullException(nameof(projection)));

            return Try(() =>
                !@this.HasValue
                && errorPredicate(@this.Error)
                    ? projection(@this.Error)
                    : @this);
        }
    }
}
