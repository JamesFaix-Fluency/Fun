using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Fun
{
    public static class TryExtensions
    {
        #region Projections

        public static Try<T2> Map<T1, T2>(//Functor Fmap
            this Try<T1> @this,
            Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            return @this.HasValue
                ? Try.Get(() => projection(@this.Value))
                : Try.Error<T2>(@this.Error);
        }

        public static Try<T2> Map<T1, T2>(//Monad Bind
            this Try<T1> @this,
            Func<T1, Try<T2>> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            return @this.HasValue
                ? Try.Get(() => projection(@this.Value))
                : Try.Error<T2>(@this.Error);
        }

        public static Try<T2> Map<T1, T2>(
           this Try<T1> @this,
           Func<T1, Try<T2>> valueProjection,
           Func<Exception, Try<T2>> errorProjection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(valueProjection, null))
                throw new ArgumentNullException(nameof(valueProjection));

            if (Equals(valueProjection, null))
                throw new ArgumentNullException(nameof(valueProjection));

            return @this.HasValue
                ? Try.Get(() => valueProjection(@this.Value))
                : Try.Get(() => errorProjection(@this.Error));
        }

        public static Task<Try<T2>> MapAsync<T1, T2>(
           this Try<T1> @this,
           Func<T1, Task<T2>> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            return @this.HasValue
                ? Try.GetAsync(() => projection(@this.Value))
                : Try.Error<T2>(@this.Error).AsTask();
        }

        public static Task<Try<T2>> MapAsync<T1, T2>(
            this Try<T1> @this,
            Func<T1, Task<Try<T2>>> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            return @this.HasValue
                ? Try.GetAsync(() => projection(@this.Value))
                : Try.Error<T2>(@this.Error).AsTask();
        }

        public static async Task<Try<T2>> MapAsync<T1, T2>(
           this Task<Try<T1>> @this,
           Func<T1, Task<Try<T2>>> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            try
            {
                var result = await @this;

                if (result.HasValue)
                {
                    return await Try.GetAsync(() => projection(result.Value));
                }
                else
                {
                    return Try.Error<T2>(result.Error);
                }
            }
            catch (Exception e)
            {
                return Try.Error<T2>(e);
            }
        }

        public static async Task<Try<T2>> MapAsync<T1, T2>(
           this Task<Try<T1>> @this,
           Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            try
            {
                var result = await @this;

                if (result.HasValue)
                {
                    return Try.Get(() => projection(result.Value));
                }
                else
                {
                    return Try.Error<T2>(result.Error);
                }
            }
            catch (Exception e)
            {
                return Try.Error<T2>(e);
            }
        }

        public static Try<IEnumerable<T2>> MapEach<T1, T2>(
            this Try<IEnumerable<T1>> @this,
            Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            return @this.HasValue
                ? Try.Get(() => @this.Value.Select(projection))
                : Try.Error<IEnumerable<T2>>(@this.Error);
        }

        public static async Task<Try<IEnumerable<T2>>> MapEachAsync<T1, T2>(
            this Task<Try<IEnumerable<T1>>> @this,
            Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            try
            {
                var result = await @this;
                if (result.HasValue)
                {
                    var value = result.Value.ToList();
                    return Try.Get(() => value.Select(projection));
                }
                else
                {
                    return Try.Error<IEnumerable<T2>>(result.Error);
                }
            }
            catch (Exception e)
            {
                return Try.Error<IEnumerable<T2>>(e);
            }
        }

        #endregion

        #region Error handling

        public static Try<T> Catch<T>(
            this Try<T> @this,
            Func<Exception, T> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            return @this.HasValue
                ? @this
                : Try.Get(() => projection(@this.Error));
        }

        public static Try<T> Catch<T>(
            this Try<T> @this,
            Func<Exception, Try<T>> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            return @this.HasValue
                ? @this
                : Try.Get(() => projection(@this.Error));
        }

        public static Try<T> Catch<T>(
            this Try<T> @this,
            Type exceptionType,
            Func<Exception, Try<T>> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            if (!@this.HasValue
                && @this.Error.GetType().IsAssignableFrom(exceptionType))
            {
                return Try.Get(() => projection(@this.Error));
            }

            return @this;
        }

        public static Try<T> Catch<T>(
           this Try<T> @this,
           Func<Exception, bool> errorPredicate,
           Func<Exception, Try<T>> projection)
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
                        return Try.Get(() => projection(@this.Error));
                    }
                    else
                    {
                        return @this;
                    }
                }
                catch (Exception e)
                {
                    return Try.Error<T>(e);
                }
            }
        }

        #endregion

        #region Assertions

        public static Try<T> Assert<T>(
            this Try<T> @this,
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
                ? Try.Error<T>(errorGenerator())
                : @this;
        }

        public static Try<T> ThrowIf<T>(
            this Try<T> @this,
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
                ? Try.Error<T>(errorGenerator())
                : @this;
        }

        #endregion

        #region Side effects

        public static Try<T> Do<T>(
            this Try<T> @this,
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
                    return Try.Error<T>(e);
                }
            }

            return @this;
        }

        public static Try<T> Do<T>(
            this Try<T> @this,
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
                    return Try.Error<T>(e);
                }
            }

            return @this;
        }

        public static Try<T> Do<T>(
            this Try<T> @this,
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
                    return Try.Error<T>(e);
                }
            }

            return @this;
        }

        public static Try<T> Do<T>(
            this Try<T> @this,
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
                    return Try.Error<T>(e);
                }
            }

            return @this;
        }

        public static Try<Unit> Ignore<T>(
            this Try<T> @this)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return Try.Some(Unit.Value);
        }

        public static async Task<Try<Unit>> IgnoreAsync<T>(
            this Task<Try<T>> @this)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            await @this;
            return Try.Some(Unit.Value);
        }

        #endregion

        #region Conversions

        public static Opt<T> AsOpt<T>(
            this Try<T> @this) =>
            @this.HasValue
                ? Opt.Some(@this.Value)
                : Opt.None<T>();

        #endregion                
    }
}
