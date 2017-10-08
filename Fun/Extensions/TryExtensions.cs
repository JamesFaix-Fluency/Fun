using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Fun
{
    public static class TryExtensions
    {
        #region Projections

        public static Try<T2> TryMap<T1, T2>(
            this Try<T1> @this,
            Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                return Try.Error<T2>(new ArgumentNullException(nameof(@this)));

            if (Equals(projection, null))
                return Try.Error<T2>(new ArgumentNullException(nameof(projection)));

            return Try.Get(() =>
                @this.Map1<T1, T2, Exception, Try<T1>, Try<T2>>(projection));
        }

        public static Try<T2> TryMap<T1, T2>(
            this Try<T1> @this,
            Func<T1, Try<T2>> projection)
        {
            if (Equals(@this, null))
                return Try.Error<T2>(new ArgumentNullException(nameof(@this)));

            if (Equals(projection, null))
                return Try.Error<T2>(new ArgumentNullException(nameof(projection)));

            return Try.Get(() =>
                @this.Map1<T1, T2, Exception, Try<T1>, Try<T2>>(projection));
        }

        public static Try<T2> TryMap<T1, T2>(
            this Try<T1> @this,
            Func<T1, Try<T2>> valueProjection,
            Func<Exception, Try<T2>> errorProjection)
        {
            if (Equals(@this, null))
                return Try.Error<T2>(new ArgumentNullException(nameof(@this)));

            if (Equals(valueProjection, null))
                return Try.Error<T2>(new ArgumentNullException(nameof(valueProjection)));

            if (Equals(errorProjection, null))
                return Try.Error<T2>(new ArgumentNullException(nameof(errorProjection)));

            return Try.Get(() =>
                @this.Map<T1, T2, Exception, Exception, Try<T1>, Try<T2>>(valueProjection, errorProjection));
        }

        public static Task<Try<T2>> TryMapAsync<T1, T2>(
            this Try<T1> @this,
            Func<T1, Task<T2>> projection)
        {
            if (Equals(@this, null))
                return Try.Error<T2>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(projection, null))
                return Try.Error<T2>(new ArgumentNullException(nameof(projection))).AsTask();

            return Try.GetAsync(() =>
                @this.Map1Async<T1, T2, Exception, Try<T1>, Try<T2>>(projection));
        }

        public static Task<Try<T2>> TryMapAsync<T1, T2>(
            this Try<T1> @this,
            Func<T1, Task<Try<T2>>> projection)
        {
            if (Equals(@this, null))
                return Try.Error<T2>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(projection, null))
                return Try.Error<T2>(new ArgumentNullException(nameof(projection))).AsTask();

            return Try.GetAsync(() =>
                @this.Map1Async<T1, T2, Exception, Try<T1>, Try<T2>>(projection));
        }

        public static Task<Try<T2>> TryMapAsync<T1, T2>(
            this Task<Try<T1>> @this,
            Func<T1, Task<Try<T2>>> projection)
        {
            if (Equals(@this, null))
                return Try.Error<T2>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(projection, null))
                return Try.Error<T2>(new ArgumentNullException(nameof(projection))).AsTask();

            return Try.GetAsync(async () => await
                (await @this).Map1Async<T1, T2, Exception, Try<T1>, Try<T2>>(projection));
        }

        public static Task<Try<T2>> TryMapAsync<T1, T2>(
            this Task<Try<T1>> @this,
            Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                return Try.Error<T2>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(projection, null))
                return Try.Error<T2>(new ArgumentNullException(nameof(projection))).AsTask();

            return Try.GetAsync(async () =>
                (await @this).Map1<T1, T2, Exception, Try<T1>, Try<T2>>(projection));
        }

        public static Try<IEnumerable<T2>> TryMapEach<T1, T2>(
            this Try<IEnumerable<T1>> @this,
            Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                return Try.Error<IEnumerable<T2>>(new ArgumentNullException(nameof(@this)));

            if (Equals(projection, null))
                return Try.Error<IEnumerable<T2>>(new ArgumentNullException(nameof(projection)));

            return Try.Get(() => @this
                .MapEach1<T1, T2, Exception, Try<IEnumerable<T1>>, Try<IEnumerable<T2>>>(projection));
        }

        public static Task<Try<IEnumerable<T2>>> TryMapEachAsync<T1, T2>(
            this Task<Try<IEnumerable<T1>>> @this,
            Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                return Try.Error<IEnumerable<T2>>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(projection, null))
                return Try.Error<IEnumerable<T2>>(new ArgumentNullException(nameof(projection))).AsTask();

            return Try.GetAsync(async () =>
                (await @this).MapEach1<T1, T2, Exception, Try<IEnumerable<T1>>, Try<IEnumerable<T2>>>(projection));
        }

        #region Error handling

        public static Try<T> Catch<T>(
            this Try<T> @this,
            Func<Exception, T> projection)
        {
            if (Equals(@this, null))
                return Try.Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(projection, null))
                return Try.Error<T>(new ArgumentNullException(nameof(projection)));

            return Try.Get(() =>
                @this.Map<T, T, Exception, Exception, Try<T>, Try<T>>(Try.Some, ex => Try.Some(projection(ex))));
        }

        public static Try<T> Catch<T>(
            this Try<T> @this,
            Func<Exception, Try<T>> projection)
        {
            if (Equals(@this, null))
                return Try.Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(projection, null))
                return Try.Error<T>(new ArgumentNullException(nameof(projection)));

            return Try.Get(() =>
                @this.Map<T, T, Exception, Exception, Try<T>, Try<T>>(Try.Some, projection));
        }

        public static Try<T> Catch<T>(
            this Try<T> @this,
            Type exceptionType,
            Func<Exception, Try<T>> projection)
        {
            if (Equals(@this, null))
                return Try.Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(exceptionType, null))
                return Try.Error<T>(new ArgumentNullException(nameof(exceptionType)));

            if (Equals(projection, null))
                return Try.Error<T>(new ArgumentNullException(nameof(projection)));

            if (!exceptionType.IsAssignableFrom(typeof(Exception)))
                return Try.Error<T>(new ArgumentException($"Exception type must extend {nameof(System)}.{nameof(Exception)}.", nameof(exceptionType)));

            return Try.Get(() =>
                @this.Map<T, T, Exception, Exception, Try<T>, Try<T>>(
                    Try.Some,
                    ex => ex.GetType().IsAssignableFrom(exceptionType)
                        ? Try.Get(() => projection(@this.Error))
                        : @this));
        }

        public static Try<T> Catch<T>(
            this Try<T> @this,
            Func<Exception, bool> errorPredicate,
            Func<Exception, Try<T>> projection)
        {
            if (Equals(@this, null))
                return Try.Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(errorPredicate, null))
                return Try.Error<T>(new ArgumentNullException(nameof(errorPredicate)));

            if (Equals(projection, null))
                return Try.Error<T>(new ArgumentNullException(nameof(projection)));

            return Try.Get(() =>
                @this.Map<T, T, Exception, Exception, Try<T>, Try<T>>(
                    Try.Some,
                    ex => errorPredicate(ex)
                        ? Try.Get(() => projection(@this.Error))
                        : @this));
        }

        #endregion

        #region Assertions

        public static Try<T> Assert<T>(
            this Try<T> @this,
            Func<T, bool> predicate,
            Func<Exception> errorGenerator)
        {
            if (Equals(@this, null))
                return Try.Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(predicate, null))
                return Try.Error<T>(new ArgumentNullException(nameof(predicate)));

            if (Equals(errorGenerator, null))
                return Try.Error<T>(new ArgumentNullException(nameof(errorGenerator)));

            return Try.Get(() =>
                @this.Map<T, T, Exception, Exception, Try<T>, Try<T>>(
                    val => predicate(val)
                        ? Try.Error<T>(errorGenerator())
                        : @this,
                    ex => @this));
        }

        public static Try<T> ThrowIf<T>( //Opposite of Assert for convenience
            this Try<T> @this,
            Func<T, bool> predicate,
            Func<Exception> errorGenerator)
        {
            if (Equals(@this, null))
                return Try.Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(predicate, null))
                return Try.Error<T>(new ArgumentNullException(nameof(predicate)));

            if (Equals(errorGenerator, null))
                return Try.Error<T>(new ArgumentNullException(nameof(errorGenerator)));

            return Try.Get(() =>
                @this.Map<T, T, Exception, Exception, Try<T>, Try<T>>(
                    val => !predicate(val)
                        ? Try.Error<T>(errorGenerator())
                        : @this,
                    ex => @this));
        }

        #endregion

        #endregion

        #region Side effects

        public static Try<T> TryDo<T>(
            this Try<T> @this,
            Func<Unit> action)
        {
            if (Equals(@this, null))
                return Try.Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(action, null))
                return Try.Error<T>(new ArgumentNullException(nameof(action)));

            return Try.Get(() => @this.Do1<T, Exception, Try<T>>(action));
        }

        public static Try<T> TryDo<T>(
            this Try<T> @this,
            Func<T, Unit> action)
        {
            if (Equals(@this, null))
                return Try.Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(action, null))
                return Try.Error<T>(new ArgumentNullException(nameof(action)));

            return Try.Get(() => @this.Do1<T, Exception, Try<T>>(action));
        }

        public static Try<T> TryDo<T>(
            this Try<T> @this,
            Action action)
        { 
            if (Equals(@this, null))
                return Try.Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(action, null))
                return Try.Error<T>(new ArgumentNullException(nameof(action)));

            return Try.Get(() => @this.Do1<T, Exception, Try<T>>(action));
        }

        public static Try<T> TryDo<T>(
            this Try<T> @this,
            Action<T> action)
        {
            if (Equals(@this, null))
                return Try.Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(action, null))
                return Try.Error<T>(new ArgumentNullException(nameof(action)));

            return Try.Get(() => @this.Do1<T, Exception, Try<T>>(action));
        }

        public static Try<Unit> Ignore<T>(
            this Try<T> @this)
        {
            if (Equals(@this, null))
                return Try.Error<Unit>(new ArgumentNullException(nameof(@this)));

            return Try.Some(Unit.Value);
        }

        public static Task<Try<Unit>> IgnoreAsync<T>(
            this Task<Try<T>> @this)
        {
            if (Equals(@this, null))
                return Try.Error<Unit>(new ArgumentNullException(nameof(@this))).AsTask();

            return Try.GetAsync(async () =>
            {
                await @this;
                return Try.Some(Unit.Value);
            });
        }

        #endregion

        #region Conversions

        public static Opt<T> AsOpt<T>(
            this Try<T> @this)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return @this.HasValue
                ? Opt.Some(@this.Value)
                : Opt.None<T>();
        }

        #endregion

        public static T Extract<T>(
            this Try<T> @this)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return @this.HasValue
                ? @this.Value
                : throw @this.Error;
        }
    }
}
