using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fun.Extensions;

namespace Fun
{
    public static partial class Result
    {
        //Functor map
        public static Result<T2> Map<T1, T2>(
            this Result<T1> @this,
            Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                return Error<T2>(new ArgumentNullException(nameof(@this)));

            if (Equals(projection, null))
                return Error<T2>(new ArgumentNullException(nameof(projection)));

            return Get(() =>
                @this.HasValue
                    ? Value(projection(@this.Value))
                    : Error<T2>(@this.Error));
        }

        //Monad bind
        public static Result<T2> Map<T1, T2>(
            this Result<T1> @this,
            Func<T1, Result<T2>> projection)
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

        public static Result<T2> Map<T1, T2>(
            this Result<T1> @this,
            Func<T1, Result<T2>> valueProjection,
            Func<Exception, Result<T2>> errorProjection)
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

        public static Task<Result<T2>> MapAsync<T1, T2>(
            this Result<T1> @this,
            Func<T1, Task<T2>> projection)
        {
            if (Equals(@this, null))
                return Error<T2>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(projection, null))
                return Error<T2>(new ArgumentNullException(nameof(projection))).AsTask();

            return GetAsync(async () =>
                @this.HasValue
                    ? Value(await projection(@this.Value))
                    : Error<T2>(@this.Error));
        }

        public static Task<Result<T2>> MapAsync<T1, T2>(
            this Result<T1> @this,
            Func<T1, Task<Result<T2>>> projection)
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

        public static Task<Result<T2>> MapAsync<T1, T2>(
            this Task<Result<T1>> @this,
            Func<T1, Task<Result<T2>>> projection)
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

        public static Task<Result<T2>> MapAsync<T1, T2>(
            this Task<Result<T1>> @this,
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
                    ? Value(projection(result.Value))
                    : Error<T2>(result.Error);
            });
        }

        public static Result<IEnumerable<T2>> MapEach<T1, T2>(
            this Result<IEnumerable<T1>> @this,
            Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                return Error<IEnumerable<T2>>(new ArgumentNullException(nameof(@this)));

            if (Equals(projection, null))
                return Error<IEnumerable<T2>>(new ArgumentNullException(nameof(projection)));

            return Get(() =>
                @this.HasValue
                    ? Value(@this.Value.Select(projection))
                    : Error<IEnumerable<T2>>(@this.Error));
        }

        public static Task<Result<IEnumerable<T2>>> MapEachAsync<T1, T2>(
            this Task<Result<IEnumerable<T1>>> @this,
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
                    ? Value(result.Value.Select(projection))
                    : Error<IEnumerable<T2>>(result.Error);
            });
        }
    }
}
