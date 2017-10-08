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

        public static Try<T2> TryMap<T1, T2>(
            this Try<T1> @this,
            Func<T1, T2> projection) =>
            Try.Get(() => @this.Map1<T1, T2, Exception, Try<T1>, Try<T2>>(projection));

        public static Try<T2> TryMap<T1, T2>(
            this Try<T1> @this,
            Func<T1, Try<T2>> projection) =>
            Try.Get(() => @this.Map1<T1, T2, Exception, Try<T1>, Try<T2>>(projection));

        public static Try<T2> TryMap<T1, T2>(
            this Try<T1> @this,
            Func<T1, Try<T2>> valueProjection,
            Func<Exception, Try<T2>> errorProjection) =>
            Try.Get(() => @this.Map<T1, T2, Exception, Exception, Try<T1>, Try<T2>>(valueProjection, errorProjection));

        public static Task<Try<T2>> TryMapAsync<T1, T2>(
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

        public static Task<Try<T2>> TryMapAsync<T1, T2>(
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

        public static async Task<Try<T2>> TryMapAsync<T1, T2>(
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

        public static async Task<Try<T2>> TryMapAsync<T1, T2>(
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

        public static Try<IEnumerable<T2>> TryMapEach<T1, T2>(
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

        public static async Task<Try<IEnumerable<T2>>> TryMapEachAsync<T1, T2>(
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
       
        #region Error handling

        public static Try<T> Catch<T>(
            this Try<T> @this,
            Func<Exception, T> projection) =>
            Try.Get(() => @this.Map<T, T, Exception, Exception, Try<T>, Try<T>>(Try.Some, ex => Try.Some(projection(ex))));

        public static Try<T> Catch<T>(
            this Try<T> @this,
            Func<Exception, Try<T>> projection) =>
            Try.Get(() => @this.Map<T, T, Exception, Exception, Try<T>, Try<T>>(Try.Some, projection));

        public static Try<T> Catch<T>(
            this Try<T> @this,
            Type exceptionType,
            Func<Exception, Try<T>> projection) =>
            Try.Get(() => @this.Map<T, T, Exception, Exception, Try<T>, Try<T>>(Try.Some, ex =>
                ex.GetType().IsAssignableFrom(exceptionType)
                    ? Try.Get(() => projection(@this.Error))
                    : @this));

        public static Try<T> Catch<T>(
            this Try<T> @this,
            Func<Exception, bool> errorPredicate,
            Func<Exception, Try<T>> projection) =>
            Try.Get(() => @this.Map<T, T, Exception, Exception, Try<T>, Try<T>>(Try.Some, ex =>
                errorPredicate(ex)
                    ? Try.Get(() => projection(@this.Error))
                    : @this));

        #endregion

        #region Assertions

        public static Try<T> Assert<T>(
            this Try<T> @this,
            Func<T, bool> predicate,
            Func<Exception> errorGenerator) =>
            Try.Get(() => @this.Map<T, T, Exception, Exception, Try<T>, Try<T>>(
                t => predicate(t)
                    ? Try.Error<T>(errorGenerator())
                    : @this,
                _ => @this));

        public static Try<T> ThrowIf<T>( //Opposite of Assert for convenience
            this Try<T> @this,
            Func<T, bool> predicate,
            Func<Exception> errorGenerator) =>
            Try.Get(() => @this.Map<T, T, Exception, Exception, Try<T>, Try<T>>(
                t => !predicate(t)
                    ? Try.Error<T>(errorGenerator())
                    : @this,
                _ => @this));

        #endregion
        
        #endregion
        
        #region Side effects

        public static Try<T> TryDo<T>(
            this Try<T> @this,
            Func<Unit> action) =>
            Try.Get(() => @this.Do1<T, Exception, Try<T>>(action));

        public static Try<T> TryDo<T>(
            this Try<T> @this,
            Func<T, Unit> action) =>
            Try.Get(() => @this.Do1<T, Exception, Try<T>>(action));

        public static Try<T> TryDo<T>(
            this Try<T> @this,
            Action action) =>
            Try.Get(() => @this.Do1<T, Exception, Try<T>>(action));

        public static Try<T> TryDo<T>(
            this Try<T> @this,
            Action<T> action) =>
            Try.Get(() => @this.Do1<T, Exception, Try<T>>(action));

        public static Try<Unit> Ignore<T>(
            this Try<T> @this)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return Try.Some(Unit.Value);
        }

        public static Task<Try<Unit>> IgnoreAsync<T>(
            this Task<Try<T>> @this)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return Try.GetAsync(async () =>
            {
                await @this;
                return Try.Some(Unit.Value);
            });
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
