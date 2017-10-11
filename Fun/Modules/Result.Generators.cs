using System;

namespace Fun
{
    public static partial class Result
    {
        /// <summary>
        /// Creates a new <see cref="Result{T}"/> with the given value.
        /// </summary>
        public static Result<T> Value<T>(T value) => new Result<T>(value);

        /// <summary>
        /// Creates a new <see cref="Result{T}"/> with the given error.
        /// </summary>
        public static Result<T> Error<T>(Exception error) => new Result<T>(error);
    }
}
