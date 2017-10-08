using System;
using System.Threading.Tasks;

namespace Fun
{
    public static class Try
    {
        #region Main generators

        /// <summary>
        /// Creates a new <see cref="Try{T}"/> with the given value.
        /// </summary>
        public static Try<T> Some<T>(T value) => new Try<T>(value);

        /// <summary>
        /// Creates a new <see cref="Try{T}"/> with the given error.
        /// </summary>
        public static Try<T> Error<T>(Exception error) => new Try<T>(error);

        #endregion

        #region Get 

        /// <summary>
        /// Calls <paramref name="generator"/> and returns <c>Some(x)</c> where <c>x</c> is the returned value of <paramref name="generator"/>. 
        /// Returns <c>Error(e)</c> if an exception is thrown, where <c>e</c> is the thrown exception.
        /// </summary>
        public static Try<T> Get<T>(Func<T> generator)
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

        /// <summary>
        /// Calls <paramref name="generator"/> and returns the result.
        /// Returns <c>Error(e)</c> if an exception is thrown, where <c>e</c> is the thrown exception.
        /// </summary>
        public static Try<T> Get<T>(Func<Try<T>> generator)
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

        /// <summary>
        /// Calls <paramref name="action"/> and returns <c>Some(Unit)</c>.
        /// Returns <c>Error(e)</c> if an exception is thrown, where <c>e</c> is the thrown exception.
        /// </summary>
        public static Try<Unit> Get(Action action)
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

        /// <summary>
        /// Awaits <paramref name="task"/>, and then returns <c>Some(x)</c> where <c>x</c> is the result of <paramref name="task"/>.
        /// Returns <c>Error(e)</c> if an exception is thrown, where <c>e</c> is the thrown exception.
        /// </summary>
        public static async Task<Try<T>> Get<T>(Task<T> task)
        {
            try
            {
                return Some(await task);
            }
            catch (Exception e)
            {
                return Error<T>(e);
            }
        }

        /// <summary>
        /// Awaits the task created by <paramref name="generator"/>, and then returns <c>Some(x)</c> where <c>x</c>
        /// is the result of the task.
        /// Returns <c>Error(e)</c> if an exception is thrown, where <c>e</c> is the thrown exception.
        /// </summary>
        public static async Task<Try<T>> GetAsync<T>(Func<Task<T>> generator)
        {
            try
            {
                return Some(await generator());
            }
            catch (Exception e)
            {
                return Error<T>(e);
            }
        }

        /// <summary>
        /// Awaits the task created by <paramref name="generator"/>, and then returns the result.
        /// Returns <c>Error(e)</c> if an exception is thrown, where <c>e</c> is the thrown exception.
        /// </summary>
        public static async Task<Try<T>> GetAsync<T>(Func<Task<Try<T>>> generator)
        {
            try
            {
                return await generator();
            }
            catch (Exception e)
            {
                return Error<T>(e);
            }
        }

        /// <summary>
        /// Awaits <paramref name="task"/>, and then returns <c>Some(Unit)</c>.
        /// Returns <c>Error(e)</c> if an exception is thrown, where <c>e</c> is the thrown exception.
        /// </summary>
        public static async Task<Try<Unit>> GetAsync(Task task)
        {
            try
            {
                await task;
                return Some(Unit.Value);
            }
            catch (Exception e)
            {
                return Error<Unit>(e);
            }
        }

        #endregion
        
        #region Assert 

        public static Try<Unit> Assert(
            bool predicate,
            Func<Exception> errorGenerator)
        {
            if (Equals(predicate, null))
                throw new ArgumentNullException(nameof(predicate));

            if (Equals(errorGenerator, null))
                throw new ArgumentNullException(nameof(errorGenerator));

            return predicate
                ? Some(Unit.Value)
                : Error<Unit>(errorGenerator());
        }

        public static Try<Unit> Assert(
            Func<bool> predicate,
            Func<Exception> errorGenerator)
        {
            if (Equals(predicate, null))
                throw new ArgumentNullException(nameof(predicate));

            if (Equals(errorGenerator, null))
                throw new ArgumentNullException(nameof(errorGenerator));

            return predicate()
                ? Some(Unit.Value)
                : Error<Unit>(errorGenerator());
        }

        #endregion


        #region Using

        public static Try<T> Using<T, TDisposable>(
            Func<TDisposable> getDisposable,
            Func<TDisposable, T> getResult)
            where TDisposable : IDisposable
        {
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

        #endregion
    }
}
