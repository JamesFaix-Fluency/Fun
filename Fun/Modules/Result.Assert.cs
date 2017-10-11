using System;

namespace Fun
{
    public static partial class Result
    {
        public static Result<unit> Assert(
            bool predicate,
            Func<Exception> errorGenerator)
        {
            if (Equals(predicate, null))
                return Error<unit>(new ArgumentNullException(nameof(predicate)));

            if (Equals(errorGenerator, null))
                return Error<unit>(new ArgumentNullException(nameof(errorGenerator)));

            return predicate
                ? Value(unit.Value)
                : Error<unit>(errorGenerator());
        }

        public static Result<unit> Assert(
            Func<bool> predicate,
            Func<Exception> errorGenerator)
        {
            if (Equals(predicate, null))
                return Error<unit>(new ArgumentNullException(nameof(predicate)));

            if (Equals(errorGenerator, null))
                return Error<unit>(new ArgumentNullException(nameof(errorGenerator)));

            return predicate()
                ? Value(unit.Value)
                : Error<unit>(errorGenerator());
        }

        public static Result<T> Assert<T>(
            this Result<T> @this,
            Func<T, bool> predicate,
            Func<Exception> errorGenerator)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(predicate, null))
                return Error<T>(new ArgumentNullException(nameof(predicate)));

            if (Equals(errorGenerator, null))
                return Error<T>(new ArgumentNullException(nameof(errorGenerator)));

            return Get(() =>
                @this.HasValue
                && !predicate(@this.Value)
                    ? Error<T>(errorGenerator())
                    : @this);
        }

        public static Result<T> ThrowIf<T>( //Opposite of Assert for convenience
            this Result<T> @this,
            Func<T, bool> predicate,
            Func<Exception> errorGenerator)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(predicate, null))
                return Error<T>(new ArgumentNullException(nameof(predicate)));

            if (Equals(errorGenerator, null))
                return Error<T>(new ArgumentNullException(nameof(errorGenerator)));

            return Get(() =>
                 @this.HasValue
                 && predicate(@this.Value)
                     ? Error<T>(errorGenerator())
                     : @this);
        }
    }
}
