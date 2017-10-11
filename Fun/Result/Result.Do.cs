using System;
using System.Threading.Tasks;
using Fun.Extensions;

namespace Fun
{
    public static partial class Result
    {
        public static result<T> Do<T>(
           this result<T> @this,
           Func<unit> action)
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

        public static result<T> Do<T>(
            this result<T> @this,
            Func<T, unit> action)
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

        public static result<T> Do<T>(
            this result<T> @this,
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

        public static result<T> Do<T>(
            this result<T> @this,
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

        public static Task<result<T>> DoAsync<T>(
            this result<T> @this,
            Func<Task<unit>> getTask)
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

        public static Task<result<T>> DoAsync<T>(
            this result<T> @this,
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

        public static Task<result<T>> DoAsync<T>(
            this result<T> @this,
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

        public static Task<result<T>> DoAsync<T>(
            this result<T> @this,
            Task<unit> task)
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

        public static Task<result<T>> DoAsync<T>(
            this result<T> @this,
            Func<T, Task<unit>> getTask)
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

        public static Task<result<T>> DoAsync<T>(
           this Task<result<T>> @this,
           Func<Task<unit>> getTask)
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

        public static Task<result<T>> DoAsync<T>(
            this Task<result<T>> @this,
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

        public static Task<result<T>> DoAsync<T>(
            this Task<result<T>> @this,
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

        public static Task<result<T>> DoAsync<T>(
            this Task<result<T>> @this,
            Task<unit> task)
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

        public static Task<result<T>> DoAsync<T>(
            this Task<result<T>> @this,
            Func<T, Task<unit>> getTask)
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
