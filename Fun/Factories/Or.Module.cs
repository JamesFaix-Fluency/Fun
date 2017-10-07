using System;

namespace Fun
{
    internal static class Or
    {
        internal static string GetInvalidItemErrorMessage(Type type, int number) =>
            $"Cannot get Item{number} from {type} unless Option is {number}.";

        internal static string GetInvalidOptionErrorMessage(Type type, int number) =>
            $"{type} cannot have an Option of {number}.";
    }

    public static class Or2
    {
        public static Or<T1, T2> First<T1, T2>(T1 value) => 
            new Or<T1, T2>(1, value, default(T2));

        public static Or<T1, T2> Second<T1, T2>(T2 value) => 
            new Or<T1, T2>(2, default(T1), value);
    }

    public static class Or3
    {
        public static Or<T1, T2, T3> First<T1, T2, T3>(T1 value) => 
            new Or<T1, T2, T3>(1, value, default(T2), default(T3));

        public static Or<T1, T2, T3> Second<T1, T2, T3>(T2 value) => 
            new Or<T1, T2, T3>(2, default(T1), value, default(T3));

        public static Or<T1, T2, T3> Third<T1, T2, T3>(T3 value) => 
            new Or<T1, T2, T3>(3, default(T1), default(T2), value);
    }
}
