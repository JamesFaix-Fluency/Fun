using System;
using System.Threading.Tasks;
using Fun.Extensions;

namespace Fun
{
    public static partial class Try
    {
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
    }
}
