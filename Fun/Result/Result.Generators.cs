using System;

namespace Fun
{
    public static partial class Result
    {
        /// <summary>
        /// Creates a new <see cref="result{T}"/> with the given value.
        /// </summary>
        public static result<T> Value<T>(T value) => new result<T>(value);

        /// <summary>
        /// Creates a new <see cref="result{T}"/> with the given error.
        /// </summary>
        public static result<T> Error<T>(Exception error) => new result<T>(error);
    }
}
