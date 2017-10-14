using System;
using System.Threading.Tasks;
using Fun.Extensions;

namespace Fun
{
    public static class Retry
    {
        public static Task<Result<T>> Get<T>(
            Func<T> getValue,
            Func<T, bool> predicate,
            Func<T, string> getErrorMessage,
            TimeSpan interval,
            int maxAttempts)
        {
            return Result.TryAsync(async () =>
            {
                /*
                 * 'value' doesn't need to be initialized here, because
                 * the loop will always run at least once, so 'value'
                 * will always be assigned to once. So it wouldn't
                 * ever be uninitialized when a TimeoutException is returned,
                 * but the compiler doesn't understand that.
                 * The point of saying this is that, 'default(T)' will never 
                 * be automatically used with '_getErrorMessage',
                 * it will always be a "real" value.
                 */
                var value = default(T);

                for (var i = 1; i <= maxAttempts; i++)
                {
                    value = getValue();

                    if (predicate(value))
                    {
                        return Result.Value(value);
                    }

                    if (i < maxAttempts)
                    {
                        await Task.Delay(interval);
                    }
                }

                return new TimeoutException(getErrorMessage(value))
                    .AsError<T>();
            });
        }

        public static Task<Result<T>> GetAsync<T>(
            Func<Task<Result<T>>> getValue,
            Func<Result<T>, bool> predicate,
            Func<Result<T>, string> getErrorMessage,
            TimeSpan interval,
            int maxAttempts)
        {
            return Result.TryAsync(async () =>
            {
                /*
                 * 'result' doesn't need to be initialized here, because
                 * the loop will always run at least once, so 'result'
                 * will always be assigned to once. So it wouldn't
                 * ever be uninitialized when a TimeoutException is returned,
                 * but the compiler doesn't understand that.
                 * The point of saying this is that, 'default(T)' will never 
                 * be automatically used with '_getErrorMessage',
                 * it will always be a "real" value.
                 */
                var result = default(T).AsResult();

                for (var i = 1; i <= maxAttempts; i++)
                {
                    result = await getValue();

                    if (predicate(result))
                    {
                        return result;
                    }
                    if (i < maxAttempts)
                    {
                        await Task.Delay(interval);
                    }
                }

                return new TimeoutException(getErrorMessage(result))
                    .AsError<T>();
            });
        }
    }
}
