using System;
using System.Threading.Tasks;
using Fun.Extensions;

namespace Fun
{
    public static partial class Result
    {
        public static Result<unit> Ignore<T>(
               this Result<T> @this)
        {
            if (Equals(@this, null))
                return Error<unit>(new ArgumentNullException(nameof(@this)));

            return Value(unit.Value);
        }

        public static Task<Result<unit>> IgnoreAsync<T>(
            this Task<Result<T>> @this)
        {
            if (Equals(@this, null))
                return Error<unit>(new ArgumentNullException(nameof(@this))).AsTask();

            return GetAsync(async () =>
            {
                await @this;
                return Value(unit.Value);
            });
        }
    }
}
