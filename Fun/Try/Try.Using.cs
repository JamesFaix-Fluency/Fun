using System;
using System.Threading.Tasks;

namespace Fun
{
    public static partial class Try
    {
        public static Try<T> Using<T, TDisposable>(
            Func<TDisposable> getDisposable,
            Func<TDisposable, T> getResult)
            where TDisposable : IDisposable
        {
            if (Equals(getDisposable, null))
                return Error<T>(new ArgumentNullException(nameof(getDisposable)));

            if (Equals(getResult, null))
                return Error<T>(new ArgumentNullException(nameof(getResult)));

            var d = default(TDisposable);

            try
            {
                d = getDisposable();
                return Some(getResult(d));
            }
            catch (Exception e)
            {
                return Error<T>(e);
            }
            finally
            {
                d?.Dispose();
            }
        }

        public static Try<T> Using<T, TDisposable>(
            Func<TDisposable> getDisposable,
            Func<TDisposable, Try<T>> getResult)
            where TDisposable : IDisposable
        {
            if (Equals(getDisposable, null))
                return Error<T>(new ArgumentNullException(nameof(getDisposable)));

            if (Equals(getResult, null))
                return Error<T>(new ArgumentNullException(nameof(getResult)));

            var d = default(TDisposable);

            try
            {
                d = getDisposable();
                return getResult(d);
            }
            catch (Exception e)
            {
                return Error<T>(e);
            }
            finally
            {
                d?.Dispose();
            }
        }

        public static async Task<Try<T>> UsingAsync<T, TDisposable>(
            Func<Task<TDisposable>> getDisposable,
            Func<TDisposable, Task<T>> getResult)
            where TDisposable : IDisposable
        {
            if (Equals(getDisposable, null))
                return Error<T>(new ArgumentNullException(nameof(getDisposable)));

            if (Equals(getResult, null))
                return Error<T>(new ArgumentNullException(nameof(getResult)));

            var d = default(TDisposable);

            try
            {
                d = await getDisposable();
                return Some(await getResult(d));
            }
            catch (Exception e)
            {
                return Error<T>(e);
            }
            finally
            {
                d?.Dispose();
            }
        }

        public static async Task<Try<T>> UsingAsync<T, TDisposable>(
            Func<Task<TDisposable>> getDisposable,
            Func<TDisposable, Task<Try<T>>> getResult)
            where TDisposable : IDisposable
        {
            if (Equals(getDisposable, null))
                return Error<T>(new ArgumentNullException(nameof(getDisposable)));

            if (Equals(getResult, null))
                return Error<T>(new ArgumentNullException(nameof(getResult)));

            var d = default(TDisposable);

            try
            {
                d = await getDisposable();
                return await getResult(d);
            }
            catch (Exception e)
            {
                return Error<T>(e);
            }
            finally
            {
                d?.Dispose();
            }
        }
    }
}
