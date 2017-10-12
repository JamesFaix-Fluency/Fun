using System;
using System.Threading.Tasks;

namespace Fun.Extensions
{
    public static class TaskExtensions
    {
        public static Task<Result<T>> AsTryAsync<T>(
            this Task<T> @this)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return Result.TryAsync(async () => await @this);
        }   
    }
}
