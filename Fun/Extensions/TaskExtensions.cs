using System;
using System.Threading.Tasks;

namespace Fun
{
    public static class TaskExtensions
    {
        public static Task<Try<T>> AsTryAsync<T>(
            this Task<T> @this)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return Try.GetAsync(async () => await @this);
        }   
    }
}
