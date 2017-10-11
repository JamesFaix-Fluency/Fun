using System;
using System.Threading.Tasks;
using Fun.Extensions;

namespace Fun
{
    public static partial class Result
    {
        public static Result<T> Do<T>(
           this Result<T> @this,
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

        public static Result<T> Do<T>(
            this Result<T> @this,
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

        public static Result<T> Do<T>(
            this Result<T> @this,
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

        public static Result<T> Do<T>(
            this Result<T> @this,
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

        public static Task<Result<T>> DoAsync<T>(
            this Result<T> @this,
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

        public static Task<Result<T>> DoAsync<T>(
            this Result<T> @this,
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

        public static Task<Result<T>> DoAsync<T>(
            this Result<T> @this,
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

        public static Task<Result<T>> DoAsync<T>(
            this Result<T> @this,
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

        public static Task<Result<T>> DoAsync<T>(
            this Result<T> @this,
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

        public static Task<Result<T>> DoAsync<T>(
           this Task<Result<T>> @this,
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

        public static Task<Result<T>> DoAsync<T>(
            this Task<Result<T>> @this,
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

        public static Task<Result<T>> DoAsync<T>(
            this Task<Result<T>> @this,
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

        public static Task<Result<T>> DoAsync<T>(
            this Task<Result<T>> @this,
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

        public static Task<Result<T>> DoAsync<T>(
            this Task<Result<T>> @this,
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
    }
}
