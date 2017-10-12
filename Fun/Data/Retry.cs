using System;
using System.Threading.Tasks;
using Fun.Extensions;

namespace Fun
{
    public class Retry<T>
    {
        private readonly Func<T> _getValue;

        private readonly Func<T, bool> _predicate;

        private readonly Func<T, string> _getErrorMessage;

        private readonly TimeSpan _interval;

        private readonly int _maxAttempts;

        internal Retry(
            Func<T> getValue,
            Func<T, bool> predicate,
            Func<T, string> getErrorMessage,
            TimeSpan interval,
            int maxAttempts)
        {
            if (getValue == null) throw new ArgumentNullException(nameof(getValue));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (getErrorMessage == null) throw new ArgumentNullException(nameof(getErrorMessage));
            if (maxAttempts < 1) throw new ArgumentOutOfRangeException(nameof(maxAttempts), "Number of attempts must be greater than zero.");
            if (getValue == null) throw new ArgumentNullException(nameof(getValue));

            _getValue = getValue;
            _predicate = predicate;
            _getErrorMessage = getErrorMessage;
            _interval = interval;
            _maxAttempts = maxAttempts;
        }

        public Task<Result<T>> Invoke()
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

                for (var i = 1; i <= _maxAttempts; i++)
                {
                    value = _getValue();

                    if (_predicate(value))
                    {
                        return Result.Value(value);
                    }

                    if (i < _maxAttempts)
                    {
                        await Task.Delay(_interval);                        
                    }
                }

                return new TimeoutException(_getErrorMessage(value)).AsError<T>();
            });
        }
    }
}
