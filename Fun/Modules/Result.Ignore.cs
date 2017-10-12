using System;
using System.Threading.Tasks;
using Fun.Extensions;

namespace Fun
{
    public static partial class Result
    {
        public static Result<Unit> Ignore<T>(
               this Result<T> @this)
        {
            if (Equals(@this, null))
                return Error<Unit>(new ArgumentNullException(nameof(@this)));

            return Value(Unit.Value);
        }

        public static Task<Result<Unit>> IgnoreAsync<T>(
            this Task<Result<T>> @this)
        {
            if (Equals(@this, null))
                return Error<Unit>(new ArgumentNullException(nameof(@this))).AsTask();

            return TryAsync(async () =>
            {
                await @this;
                return Value(Unit.Value);
            });
        }
    }
}
