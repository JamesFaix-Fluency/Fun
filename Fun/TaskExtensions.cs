using System;
using System.Threading.Tasks;

namespace Fun
{
    public static class TaskExtensions
    {
        public static async Task<Result<T>> GetResult<T>(
            this Task<T> @this)
        {
            try
            {
                return Result.Some(await @this);
            }
            catch (Exception e)
            {
                return Result.Error<T>(e);
            }
        }
    }
}
