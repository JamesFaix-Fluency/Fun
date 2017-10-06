using System;
using System.Reflection;

namespace Fun
{
    public static class ResultExtensions
    {  
        #region Projections

        public static Result<T2> Map<T1, T2>(//Functor Fmap
            this Result<T1> @this,
            Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            return @this.HasValue
                ? Result.Try(() => projection(@this.Value))
                : Result.Error<T2>(@this.Error);
        }

        public static Result<T2> Map<T1, T2>(//Monad Bind
            this Result<T1> @this,
            Func<T1, Result<T2>> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            return @this.HasValue
                ? Result.Try(() => projection(@this.Value))
                : Result.Error<T2>(@this.Error);
        }

        public static Result<T2> Map<T1, T2>(
           this Result<T1> @this,
           Func<T1, Result<T2>> valueProjection,
           Func<Exception, Result<T2>> errorProjection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(valueProjection, null))
                throw new ArgumentNullException(nameof(valueProjection));

            if (Equals(valueProjection, null))
                throw new ArgumentNullException(nameof(valueProjection));

            return @this.HasValue
                ? Result.Try(() => valueProjection(@this.Value))
                : Result.Try(() => errorProjection(@this.Error));
        }

        public static Result<T> Catch<T>(
            this Result<T> @this,
            Func<Exception, T> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            return @this.HasValue
                ? @this
                : Result.Try(() => projection(@this.Error));
        }

        public static Result<T> Catch<T>(
            this Result<T> @this,
            Func<Exception, Result<T>> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            return @this.HasValue
                ? @this
                : Result.Try(() => projection(@this.Error));
        }

        public static Result<T> Catch<T>(
            this Result<T> @this,
            Type exceptionType,
            Func<Exception, Result<T>> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            if (!@this.HasValue
                && @this.Error.GetType().IsAssignableFrom(exceptionType))
            {
                return Result.Try(() => projection(@this.Error));
            }

            return @this;
        }

        public static Result<T> Catch<T>(
           this Result<T> @this,
           Func<Exception, bool> errorPredicate,
           Func<Exception, Result<T>> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            if (@this.HasValue)
            {
                return @this;
            }
            else
            {
                try
                {
                    if (errorPredicate(@this.Error))
                    {
                        return Result.Try(() => projection(@this.Error));
                    }
                    else
                    {
                        return @this;
                    }
                }
                catch (Exception e)
                {
                    return Result.Error<T>(e);
                }
            }
        }

        public static Result<T> Assert<T>(
            this Result<T> @this,
            Func<T, bool> predicate,
            Func<Exception> errorGenerator)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(predicate, null))
                throw new ArgumentNullException(nameof(predicate));

            if (Equals(errorGenerator, null))
                throw new ArgumentNullException(nameof(errorGenerator));

            return @this.HasValue && !predicate(@this.Value)
                ? Result.Error<T>(errorGenerator())
                : @this;
        }

        public static Result<T> ThrowIf<T>(
            this Result<T> @this,
            Func<T, bool> predicate,
            Func<Exception> errorGenerator)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(predicate, null))
                throw new ArgumentNullException(nameof(predicate));

            if (Equals(errorGenerator, null))
                throw new ArgumentNullException(nameof(errorGenerator));

            return @this.HasValue && predicate(@this.Value)
                ? Result.Error<T>(errorGenerator())
                : @this;
        }

        #endregion

        #region Side effects

        public static Result<T> Do<T>(
            this Result<T> @this,
            Func<Unit> action)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.HasValue)
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    return Result.Error<T>(e);
                }
            }

            return @this;
        }

        public static Result<T> Do<T>(
            this Result<T> @this,
            Func<T, Unit> action)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.HasValue)
            {
                try
                {
                    action(@this.Value);
                }
                catch (Exception e)
                {
                    return Result.Error<T>(e);
                }
            }

            return @this;
        }

        public static Result<T> Do<T>(
            this Result<T> @this,
            Action action)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.HasValue)
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    return Result.Error<T>(e);
                }
            }

            return @this;
        }

        public static Result<T> Do<T>(
            this Result<T> @this,
            Action<T> action)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.HasValue)
            {
                try
                {
                    action(@this.Value);
                }
                catch (Exception e)
                {
                    return Result.Error<T>(e);
                }
            }

            return @this;
        }

        public static Result<Unit> Ignore<T>(
            this Result<T> @this)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return Result.Some(Unit.Value);
        }

        #endregion

        #region Conversions

        public static Maybe<T> AsMaybe<T>(
            this Result<T> @this) =>
            @this.HasValue
                ? Maybe.Some(@this.Value)
                : Maybe.None<T>();

        #endregion
    }
}
