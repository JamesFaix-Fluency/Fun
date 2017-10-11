using System;
using System.Threading.Tasks;
using Fun.Extensions;

namespace Fun
{
    public static partial class Result
    {
        public static result<unit> Ignore<T>(
               this result<T> @this)
        {
            if (Equals(@this, null))
                return Error<unit>(new ArgumentNullException(nameof(@this)));

            return Value(unit.Value);
        }

        public static Task<result<unit>> IgnoreAsync<T>(
            this Task<result<T>> @this)
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
