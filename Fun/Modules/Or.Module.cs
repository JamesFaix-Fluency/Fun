using System;

namespace Fun
{
    internal static class Or
    {
        internal static string GetInvalidItemErrorMessage(Type type, int number) =>
            $"Cannot get Item{number} from {type} unless Tag is {number}.";

        internal static string GetInvalidTagErrorMessage(Type type, int number) =>
            $"{type} cannot have a Tag of {number}.";
    }

    public static class Or2
    {
        /// <summary>
        /// Creates a new <see cref="Or{T1, T2}"/> with the given value and <c>Tag = 1</c>.
        /// </summary>
        public static Or<T1, T2> First<T1, T2>(T1 value) => 
            new Or<T1, T2>(1, value, default(T2));

        /// <summary>
        /// Creates a new <see cref="Or{T1, T2}"/> with the given value and <c>Tag = 2</c>.
        /// </summary>
        public static Or<T1, T2> Second<T1, T2>(T2 value) => 
            new Or<T1, T2>(2, default(T1), value);
    }

    public static class Or3
    {
        /// <summary>
        /// Creates a new <see cref="Or{T1, T2, T3}"/> with the given value and <c>Tag = 1</c>.
        /// </summary>
        public static Or<T1, T2, T3> First<T1, T2, T3>(T1 value) => 
            new Or<T1, T2, T3>(1, value, default(T2), default(T3));

        /// <summary>
        /// Creates a new <see cref="Or{T1, T2, T3}"/> with the given value and <c>Tag = 2</c>.
        /// </summary>
        public static Or<T1, T2, T3> Second<T1, T2, T3>(T2 value) => 
            new Or<T1, T2, T3>(2, default(T1), value, default(T3));

        /// <summary>
        /// Creates a new <see cref="Or{T1, T2, T3}"/> with the given value and <c>Tag = 3</c>.
        /// </summary>    
        public static Or<T1, T2, T3> Third<T1, T2, T3>(T3 value) => 
            new Or<T1, T2, T3>(3, default(T1), default(T2), value);
    }
}
