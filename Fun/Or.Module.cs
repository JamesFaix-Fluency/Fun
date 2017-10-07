namespace Fun
{
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
            new Or<T1, T2, T3>(2, default(T1), default(T2), value);
    }
}
