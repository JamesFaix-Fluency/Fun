using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fun.Extensions;

namespace Fun
{
    public static partial class Try
    {
        //Functor map
        public static Try<T2> TryMap<T1, T2>(
            this Try<T1> @this,
            Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                return Error<T2>(new ArgumentNullException(nameof(@this)));

            if (Equals(projection, null))
                return Error<T2>(new ArgumentNullException(nameof(projection)));

            return Get(() =>
                @this.HasValue
                    ? Some(projection(@this.Value))
                    : Error<T2>(@this.Error));
        }

        //Monad bind
        public static Try<T2> TryMap<T1, T2>(
            this Try<T1> @this,
            Func<T1, Try<T2>> projection)
        {
            if (Equals(@this, null))
                return Error<T2>(new ArgumentNullException(nameof(@this)));

            if (Equals(projection, null))
                return Error<T2>(new ArgumentNullException(nameof(projection)));

            return Get(() =>
                @this.HasValue
                    ? projection(@this.Value)
                    : Error<T2>(@this.Error));
        }

        public static Try<T2> TryMap<T1, T2>(
            this Try<T1> @this,
            Func<T1, Try<T2>> valueProjection,
            Func<Exception, Try<T2>> errorProjection)
        {
            if (Equals(@this, null))
                return Error<T2>(new ArgumentNullException(nameof(@this)));

            if (Equals(valueProjection, null))
                return Error<T2>(new ArgumentNullException(nameof(valueProjection)));

            if (Equals(errorProjection, null))
                return Error<T2>(new ArgumentNullException(nameof(errorProjection)));

            return Get(() =>
                @this.HasValue
                    ? valueProjection(@this.Value)
                    : errorProjection(@this.Error));
        }

        public static Task<Try<T2>> TryMapAsync<T1, T2>(
            this Try<T1> @this,
            Func<T1, Task<T2>> projection)
        {
            if (Equals(@this, null))
                return Error<T2>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(projection, null))
                return Error<T2>(new ArgumentNullException(nameof(projection))).AsTask();

            return GetAsync(async () =>
                @this.HasValue
                    ? Some(await projection(@this.Value))
                    : Error<T2>(@this.Error));
        }

        public static Task<Try<T2>> TryMapAsync<T1, T2>(
            this Try<T1> @this,
            Func<T1, Task<Try<T2>>> projection)
        {
            if (Equals(@this, null))
                return Error<T2>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(projection, null))
                return Error<T2>(new ArgumentNullException(nameof(projection))).AsTask();

            return GetAsync(async () =>
                @this.HasValue
                    ? await projection(@this.Value)
                    : Error<T2>(@this.Error));
        }

        public static Task<Try<T2>> TryMapAsync<T1, T2>(
            this Task<Try<T1>> @this,
            Func<T1, Task<Try<T2>>> projection)
        {
            if (Equals(@this, null))
                return Error<T2>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(projection, null))
                return Error<T2>(new ArgumentNullException(nameof(projection))).AsTask();

            return GetAsync(async () =>
            {
                var result = await @this;
                return result.HasValue
                    ? await projection(result.Value)
                    : Error<T2>(result.Error);
            });
        }

        public static Task<Try<T2>> TryMapAsync<T1, T2>(
            this Task<Try<T1>> @this,
            Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                return Error<T2>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(projection, null))
                return Error<T2>(new ArgumentNullException(nameof(projection))).AsTask();

            return GetAsync(async () =>
            {
                var result = await @this;
                return result.HasValue
                    ? Some(projection(result.Value))
                    : Error<T2>(result.Error);
            });
        }

        public static Try<IEnumerable<T2>> TryMapEach<T1, T2>(
            this Try<IEnumerable<T1>> @this,
            Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                return Error<IEnumerable<T2>>(new ArgumentNullException(nameof(@this)));

            if (Equals(projection, null))
                return Error<IEnumerable<T2>>(new ArgumentNullException(nameof(projection)));

            return Get(() =>
                @this.HasValue
                    ? Some(@this.Value.Select(projection))
                    : Error<IEnumerable<T2>>(@this.Error));
        }

        public static Task<Try<IEnumerable<T2>>> TryMapEachAsync<T1, T2>(
            this Task<Try<IEnumerable<T1>>> @this,
            Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                return Error<IEnumerable<T2>>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(projection, null))
                return Error<IEnumerable<T2>>(new ArgumentNullException(nameof(projection))).AsTask();

            return GetAsync(async () =>
            {
                var result = await @this;
                return result.HasValue
                    ? Some(result.Value.Select(projection))
                    : Error<IEnumerable<T2>>(result.Error);
            });
        }
    }
}
