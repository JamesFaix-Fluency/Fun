using System;

namespace Fun
{
    public static partial class Try
    {
        public static Try<Unit> Assert(
            bool predicate,
            Func<Exception> errorGenerator)
        {
            if (Equals(predicate, null))
                return Error<Unit>(new ArgumentNullException(nameof(predicate)));

            if (Equals(errorGenerator, null))
                return Error<Unit>(new ArgumentNullException(nameof(errorGenerator)));

            return predicate
                ? Some(Unit.Value)
                : Error<Unit>(errorGenerator());
        }

        public static Try<Unit> Assert(
            Func<bool> predicate,
            Func<Exception> errorGenerator)
        {
            if (Equals(predicate, null))
                return Error<Unit>(new ArgumentNullException(nameof(predicate)));

            if (Equals(errorGenerator, null))
                return Error<Unit>(new ArgumentNullException(nameof(errorGenerator)));

            return predicate()
                ? Some(Unit.Value)
                : Error<Unit>(errorGenerator());
        }

        public static Try<T> Assert<T>(
            this Try<T> @this,
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

        public static Try<T> ThrowIf<T>( //Opposite of Assert for convenience
            this Try<T> @this,
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
