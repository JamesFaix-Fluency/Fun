using System;
using System.Threading.Tasks;

namespace Fun
{
    public static class Retry
    {
        public static Retry<T> Create<T>(
            Func<T> getValue,
            Func<T, bool> predicate,
            Func<T, string> getErrorMessage,
            TimeSpan interval,
            int maxAttempts)
        {
            return new Retry<T>(
                getValue,
                predicate,
                getErrorMessage,
                interval,
                maxAttempts);
        }

        public static Task<Result<T>> Get<T>(
            Func<T> getValue,
            Func<T, bool> predicate,
            Func<T, string> getErrorMessage,
            TimeSpan interval,
            int maxAttempts)
        {
            var retry = new Retry<T>(
                getValue,
                predicate,
                getErrorMessage,
                interval,
                maxAttempts);

            return retry.Invoke();
        }
    }
}
