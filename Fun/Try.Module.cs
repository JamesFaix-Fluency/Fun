using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            if (Equals(generator, null))
                return Error<T>(new ArgumentNullException(nameof(generator)));

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
            if (Equals(generator, null))
                return Error<T>(new ArgumentNullException(nameof(generator)));

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
            if (Equals(action, null))
                return Error<Unit>(new ArgumentNullException(nameof(action)));

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
            if (Equals(task, null))
                return Error<T>(new ArgumentNullException(nameof(task)));

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
            if (Equals(generator, null))
                return Error<T>(new ArgumentNullException(nameof(generator)));

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
            if (Equals(generator, null))
                return Error<T>(new ArgumentNullException(nameof(generator)));

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
            if (Equals(task, null))
                return Error<Unit>(new ArgumentNullException(nameof(task)));

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
        
        /// <summary>
        /// Awaits the task created by <paramref name="generator"/>, and then returns <c>Some(Unit)</c>.
        /// Returns <c>Error(e)</c> if an exception is thrown, where <c>e</c> is the thrown exception.
        /// </summary>
        public static async Task<Try<Unit>> GetAsync(Func<Task> generator)
        {
            if (Equals(generator, null))
                return Error<Unit>(new ArgumentNullException(nameof(generator)));

            try
            {
                await generator();
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
                return Error<Unit>(new ArgumentNullException(nameof(predicate)));

            if (Equals(errorGenerator, null))
                return Error<Unit>(new ArgumentNullException(nameof(errorGenerator)));

            return predicate
                ? Some(Unit.Value)
                : Error<Unit>(errorGenerator());
        }

        public static Try<Unit> Assert(
            Func<bool> predicate,
            Func<Exception> errorGenerator)
        {
            if (Equals(predicate, null))
                return Error<Unit>(new ArgumentNullException(nameof(predicate)));

            if (Equals(errorGenerator, null))
                return Error<Unit>(new ArgumentNullException(nameof(errorGenerator)));

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

        #endregion
        #region Projections

        //Functor map
        public static Try<T2> TryMap<T1, T2>(
            this Try<T1> @this,
            Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                return Error<T2>(new ArgumentNullException(nameof(@this)));

            if (Equals(projection, null))
                return Error<T2>(new ArgumentNullException(nameof(projection)));

            return Get(() =>
                @this.HasValue
                    ? Some(projection(@this.Value))
                    : Error<T2>(@this.Error));
        }

        //Monad bind
        public static Try<T2> TryMap<T1, T2>(
            this Try<T1> @this,
            Func<T1, Try<T2>> projection)
        {
            if (Equals(@this, null))
                return Error<T2>(new ArgumentNullException(nameof(@this)));

            if (Equals(projection, null))
                return Error<T2>(new ArgumentNullException(nameof(projection)));

            return Get(() =>
                @this.HasValue
                    ? projection(@this.Value)
                    : Error<T2>(@this.Error));
        }

        public static Try<T2> TryMap<T1, T2>(
            this Try<T1> @this,
            Func<T1, Try<T2>> valueProjection,
            Func<Exception, Try<T2>> errorProjection)
        {
            if (Equals(@this, null))
                return Error<T2>(new ArgumentNullException(nameof(@this)));

            if (Equals(valueProjection, null))
                return Error<T2>(new ArgumentNullException(nameof(valueProjection)));

            if (Equals(errorProjection, null))
                return Error<T2>(new ArgumentNullException(nameof(errorProjection)));

            return Get(() =>
                @this.HasValue
                    ? valueProjection(@this.Value)
                    : errorProjection(@this.Error));
        }

        public static Task<Try<T2>> TryMapAsync<T1, T2>(
            this Try<T1> @this,
            Func<T1, Task<T2>> projection)
        {
            if (Equals(@this, null))
                return Error<T2>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(projection, null))
                return Error<T2>(new ArgumentNullException(nameof(projection))).AsTask();

            return GetAsync(async () =>
                @this.HasValue
                    ? Some(await projection(@this.Value))
                    : Error<T2>(@this.Error));
        }

        public static Task<Try<T2>> TryMapAsync<T1, T2>(
            this Try<T1> @this,
            Func<T1, Task<Try<T2>>> projection)
        {
            if (Equals(@this, null))
                return Error<T2>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(projection, null))
                return Error<T2>(new ArgumentNullException(nameof(projection))).AsTask();

            return GetAsync(async () =>
                @this.HasValue
                    ? await projection(@this.Value)
                    : Error<T2>(@this.Error));
        }

        public static Task<Try<T2>> TryMapAsync<T1, T2>(
            this Task<Try<T1>> @this,
            Func<T1, Task<Try<T2>>> projection)
        {
            if (Equals(@this, null))
                return Error<T2>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(projection, null))
                return Error<T2>(new ArgumentNullException(nameof(projection))).AsTask();

            return GetAsync(async () =>
            {
                var result = await @this;
                return result.HasValue
                    ? await projection(result.Value)
                    : Error<T2>(result.Error);
            });
        }

        public static Task<Try<T2>> TryMapAsync<T1, T2>(
            this Task<Try<T1>> @this,
            Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                return Error<T2>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(projection, null))
                return Error<T2>(new ArgumentNullException(nameof(projection))).AsTask();

            return GetAsync(async () =>
            {
                var result = await @this;
                return result.HasValue
                    ? Some(projection(result.Value))
                    : Error<T2>(result.Error);
            });
        }

        public static Try<IEnumerable<T2>> TryMapEach<T1, T2>(
            this Try<IEnumerable<T1>> @this,
            Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                return Error<IEnumerable<T2>>(new ArgumentNullException(nameof(@this)));

            if (Equals(projection, null))
                return Error<IEnumerable<T2>>(new ArgumentNullException(nameof(projection)));

            return Get(() =>
                @this.HasValue
                    ? Some(@this.Value.Select(projection))
                    : Error<IEnumerable<T2>>(@this.Error));
        }

        public static Task<Try<IEnumerable<T2>>> TryMapEachAsync<T1, T2>(
            this Task<Try<IEnumerable<T1>>> @this,
            Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                return Error<IEnumerable<T2>>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(projection, null))
                return Error<IEnumerable<T2>>(new ArgumentNullException(nameof(projection))).AsTask();

            return GetAsync(async () =>
            {
                var result = await @this;
                return result.HasValue
                    ? Some(result.Value.Select(projection))
                    : Error<IEnumerable<T2>>(result.Error);
            });
        }

        #region Error handling

        public static Try<T> Catch<T>(
            this Try<T> @this,
            Func<Exception, T> projection)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(projection, null))
                return Error<T>(new ArgumentNullException(nameof(projection)));

            return Get(() =>
                @this.HasValue
                    ? @this
                    : Some(projection(@this.Error)));
        }

        public static Try<T> Catch<T>(
            this Try<T> @this,
            Func<Exception, Try<T>> projection)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(projection, null))
                return Error<T>(new ArgumentNullException(nameof(projection)));

            return Get(() =>
                @this.HasValue
                    ? @this
                    : projection(@this.Error));
        }

        public static Try<T> Catch<T>(
            this Try<T> @this,
            Type exceptionType,
            Func<Exception, Try<T>> projection)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(exceptionType, null))
                return Error<T>(new ArgumentNullException(nameof(exceptionType)));

            if (Equals(projection, null))
                return Error<T>(new ArgumentNullException(nameof(projection)));

            if (!exceptionType.IsAssignableFrom(typeof(Exception)))
                return Error<T>(new ArgumentException($"Exception type must extend {nameof(System)}.{nameof(Exception)}.", nameof(exceptionType)));

            return Get(() =>
                !@this.HasValue
                && @this.Error.GetType().IsAssignableFrom(exceptionType)
                    ? projection(@this.Error)
                    : @this);
        }

        public static Try<T> Catch<T>(
            this Try<T> @this,
            Func<Exception, bool> errorPredicate,
            Func<Exception, Try<T>> projection)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(errorPredicate, null))
                return Error<T>(new ArgumentNullException(nameof(errorPredicate)));

            if (Equals(projection, null))
                return Error<T>(new ArgumentNullException(nameof(projection)));

            return Get(() =>
                !@this.HasValue
                && errorPredicate(@this.Error)
                    ? projection(@this.Error)
                    : @this);
        }

        #endregion

        #region Assertions

        public static Try<T> Assert<T>(
            this Try<T> @this,
            Func<T, bool> predicate,
            Func<Exception> errorGenerator)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(predicate, null))
                return Error<T>(new ArgumentNullException(nameof(predicate)));

            if (Equals(errorGenerator, null))
                return Error<T>(new ArgumentNullException(nameof(errorGenerator)));

            return Get(() =>
                @this.HasValue
                && !predicate(@this.Value)
                    ? Error<T>(errorGenerator())
                    : @this);
        }

        public static Try<T> ThrowIf<T>( //Opposite of Assert for convenience
            this Try<T> @this,
            Func<T, bool> predicate,
            Func<Exception> errorGenerator)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(predicate, null))
                return Error<T>(new ArgumentNullException(nameof(predicate)));

            if (Equals(errorGenerator, null))
                return Error<T>(new ArgumentNullException(nameof(errorGenerator)));

            return Get(() =>
                 @this.HasValue
                 && predicate(@this.Value)
                     ? Error<T>(errorGenerator())
                     : @this);
        }

        #endregion

        #endregion

        #region Side effects

        public static Try<T> TryDo<T>(
            this Try<T> @this,
            Func<Unit> action)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(action, null))
                return Error<T>(new ArgumentNullException(nameof(action)));

            return Get(() =>
            {
                if (@this.HasValue)
                {
                    action();
                }
                return @this;
            });
        }

        public static Try<T> TryDo<T>(
            this Try<T> @this,
            Func<T, Unit> action)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(action, null))
                return Error<T>(new ArgumentNullException(nameof(action)));

            return Get(() =>
            {
                if (@this.HasValue)
                {
                    action(@this.Value);
                }
                return @this;
            });
        }

        public static Try<T> TryDo<T>(
            this Try<T> @this,
            Action action)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(action, null))
                return Error<T>(new ArgumentNullException(nameof(action)));

            return Get(() =>
            {
                if (@this.HasValue)
                {
                    action();
                }
                return @this;
            });
        }

        public static Try<T> TryDo<T>(
            this Try<T> @this,
            Action<T> action)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this)));

            if (Equals(action, null))
                return Error<T>(new ArgumentNullException(nameof(action)));

            return Get(() =>
            {
                if (@this.HasValue)
                {
                    action(@this.Value);
                }
                return @this;
            });
        }

        public static Task<Try<T>> TryDoAsync<T>(
            this Try<T> @this,
            Func<Task<Unit>> getTask)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(getTask, null))
                return Error<T>(new ArgumentNullException(nameof(getTask))).AsTask();

            return GetAsync(async () =>
            {
                if (@this.HasValue)
                {
                    await getTask();
                }
                return @this;
            });
        }

        public static Task<Try<T>> TryDoAsync<T>(
            this Try<T> @this,
            Func<T, Task> getTask)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(getTask, null))
                return Error<T>(new ArgumentNullException(nameof(getTask))).AsTask();

            return GetAsync(async () =>
            {
                if (@this.HasValue)
                {
                    await getTask(@this.Value);
                }
                return @this;
            });
        }

        public static Task<Try<T>> TryDoAsync<T>(
            this Try<T> @this,
            Task task)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(task, null))
                return Error<T>(new ArgumentNullException(nameof(task))).AsTask();

            return GetAsync(async () =>
            {
                if (@this.HasValue)
                {
                    await task;
                }
                return @this;
            });
        }

        public static Task<Try<T>> TryDoAsync<T>(
            this Try<T> @this,
            Task<Unit> task)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(task, null))
                return Error<T>(new ArgumentNullException(nameof(task))).AsTask();

            return GetAsync(async () =>
            {
                if (@this.HasValue)
                {
                    await task;
                }
                return @this;
            });
        }

        public static Task<Try<T>> TryDoAsync<T>(
            this Try<T> @this,
            Func<T, Task<Unit>> getTask)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(getTask, null))
                return Error<T>(new ArgumentNullException(nameof(getTask))).AsTask();

            return GetAsync(async () =>
            {
                if (@this.HasValue)
                {
                    await getTask(@this.Value);
                }
                return @this;
            });
        }

        public static Task<Try<T>> TryDoAsync<T>(
           this Task<Try<T>> @this,
           Func<Task<Unit>> getTask)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(getTask, null))
                return Error<T>(new ArgumentNullException(nameof(getTask))).AsTask();

            return GetAsync(async () =>
            {
                var result = await @this;
                if (result.HasValue)
                {
                    await getTask();
                }
                return result;
            });
        }

        public static Task<Try<T>> TryDoAsync<T>(
            this Task<Try<T>> @this,
            Func<T, Task> getTask)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(getTask, null))
                return Error<T>(new ArgumentNullException(nameof(getTask))).AsTask();

            return GetAsync(async () =>
            {
                var result = await @this;
                if (result.HasValue)
                {
                    await getTask(result.Value);
                }
                return result;
            });
        }

        public static Task<Try<T>> TryDoAsync<T>(
            this Task<Try<T>> @this,
            Task task)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(task, null))
                return Error<T>(new ArgumentNullException(nameof(task))).AsTask();

            return GetAsync(async () =>
            {
                var result = await @this;
                if (result.HasValue)
                {
                    await task;
                }
                return result;
            });
        }

        public static Task<Try<T>> TryDoAsync<T>(
            this Task<Try<T>> @this,
            Task<Unit> task)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(task, null))
                return Error<T>(new ArgumentNullException(nameof(task))).AsTask();

            return GetAsync(async () =>
            {
                var result = await @this;
                if (result.HasValue)
                {
                    await task;
                }
                return result;
            });
        }

        public static Task<Try<T>> TryDoAsync<T>(
            this Task<Try<T>> @this,
            Func<T, Task<Unit>> getTask)
        {
            if (Equals(@this, null))
                return Error<T>(new ArgumentNullException(nameof(@this))).AsTask();

            if (Equals(getTask, null))
                return Error<T>(new ArgumentNullException(nameof(getTask))).AsTask();

            return GetAsync(async () =>
            {
                var result = await @this;
                if (result.HasValue)
                {
                    await getTask(result.Value);
                }
                return result;
            });
        }

        public static Try<Unit> Ignore<T>(
            this Try<T> @this)
        {
            if (Equals(@this, null))
                return Error<Unit>(new ArgumentNullException(nameof(@this)));

            return Some(Unit.Value);
        }

        public static Task<Try<Unit>> IgnoreAsync<T>(
            this Task<Try<T>> @this)
        {
            if (Equals(@this, null))
                return Error<Unit>(new ArgumentNullException(nameof(@this))).AsTask();

            return GetAsync(async () =>
            {
                await @this;
                return Some(Unit.Value);
            });
        }

        #endregion

        #region Conversions

        public static Opt<T> AsOpt<T>(
            this Try<T> @this)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return @this.HasValue
                ? Opt.Some(@this.Value)
                : Opt.None<T>();
        }

        #endregion

        public static T Extract<T>(
            this Try<T> @this)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return @this.HasValue
                ? @this.Value
                : throw @this.Error;
        }
    }
}
