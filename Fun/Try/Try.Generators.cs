using System;

namespace Fun
{
    public static partial class Try
    {
        /// <summary>
        /// Creates a new <see cref="Try{T}"/> with the given value.
        /// </summary>
        public static Try<T> Some<T>(T value) => new Try<T>(value);

        /// <summary>
        /// Creates a new <see cref="Try{T}"/> with the given error.
        /// </summary>
        public static Try<T> Error<T>(Exception error) => new Try<T>(error);
    }
}
