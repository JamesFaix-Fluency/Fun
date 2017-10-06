using System;
using System.Threading.Tasks;

namespace Fun
{
    public static class Result
    {
        #region Main generators

        public static Result<T> Some<T>(
            T value) =>
            new Result<T>(value, null);

        public static Result<T> Error<T>(
            Exception error) =>
            new Result<T>(default(T), error);

        #endregion

        public static Result<T> Try<T>(
            Func<T> generator)
        {
            try
            {
                return Some(generator());
            }
            catch (Exception e)
            {
                return Error<T>(e);
            }
        }

        public static Result<T> Try<T>(
            Func<Result<T>> generator)
        {
            try
            {
                return generator();
            }
            catch (Exception e)
            {
                return Error<T>(e);
            }
        }

        public static Result<Unit> Try(
            Action action)
        {
            try
            {
                action();
                return Some(Unit.Value);
            }
            catch (Exception e)
            {
                return Error<Unit>(e);
            }
        }

        public static async Task<Result<T>> Try<T>(
            Task<T> @this)
        {
            try
            {
                return Some(await @this);
            }
            catch (Exception e)
            {
                return Error<T>(e);
            }
        }

        public static Result<T> TryUsing<T, TDisposable>(
            Func<TDisposable> getDisposable,
            Func<TDisposable, T> projection)
            where TDisposable : IDisposable
        {
            var disp = default(TDisposable);

            try
            {
                disp = getDisposable();
                return Some(projection(disp));
            }
            catch (Exception e)
            {
                return Error<T>(e);
            }
            finally
            {
                disp?.Dispose();
            }
        }
    }
}
