using System;

namespace Fun
{
    public static partial class Result
    {
        public static result<unit> Assert(
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

        public static result<unit> Assert(
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

        public static result<T> Assert<T>(
            this result<T> @this,
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

        public static result<T> ThrowIf<T>( //Opposite of Assert for convenience
            this result<T> @this,
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
