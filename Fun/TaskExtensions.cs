using System;
using System.Threading.Tasks;

namespace Fun
{
    public static class TaskExtensions
    {
        public static async Task<Try<T>> GetResultAsync<T>(
            this Task<T> @this)
        {
            try
            {
                return Try.Some(await @this);
            }
            catch (Exception e)
            {
                return Try.Error<T>(e);
            }
        }   
    }
}
