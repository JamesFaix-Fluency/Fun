using System;
using System.Threading.Tasks;

namespace Fun
{
    public static partial class Result
    {
        public static Result<T> Using<T, TDisposable>(
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
                return Value(getResult(d));
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

        public static Result<T> Using<T, TDisposable>(
            Func<TDisposable> getDisposable,
            Func<TDisposable, Result<T>> getResult)
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

        public static async Task<Result<T>> UsingAsync<T, TDisposable>(
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
                return Value(await getResult(d));
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

        public static async Task<Result<T>> UsingAsync<T, TDisposable>(
            Func<Task<TDisposable>> getDisposable,
            Func<TDisposable, Task<Result<T>>> getResult)
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

        public static async Task<Result<Unit>> UsingAsync<TDisposable>(
            Func<TDisposable> getDisposable,
            Action<TDisposable> action)
            where TDisposable : IDisposable
        {
            if (Equals(getDisposable, null))
                return Error<Unit>(new ArgumentNullException(nameof(getDisposable)));

            if (Equals(action, null))
                return Error<Unit>(new ArgumentNullException(nameof(action)));

            var d = default(TDisposable);
            
            try
            {
                d = getDisposable();
                action(d);
                return Value(Unit.Value);
            }
            catch (Exception e)
            {
                return Error<Unit>(e);
            }
            finally
            {
                d?.Dispose();
            }
        }

        public static async Task<Result<T>> UsingAsync<T, TDisposable>(
            Func<TDisposable> getDisposable,
            Func<TDisposable, Task<Result<T>>> getResult)
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
