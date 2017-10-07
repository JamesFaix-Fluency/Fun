namespace Fun
{
    internal class Or2Factory : IOr2Factory
    {
        public Or<T1, T2> First<T1, T2>(T1 value) =>
           new Or<T1, T2>(1, value, default(T2));

        public Or<T1, T2> Second<T1, T2>(T2 value) =>
            new Or<T1, T2>(2, default(T1), value);
    }

    internal class Or3Factory : IOr3Factory
    {
        public Or<T1, T2, T3> First<T1, T2, T3>(T1 value) =>
            new Or<T1, T2, T3>(1, value, default(T2), default(T3));

        public Or<T1, T2, T3> Second<T1, T2, T3>(T2 value) =>
            new Or<T1, T2, T3>(2, default(T1), value, default(T3));

        public Or<T1, T2, T3> Third<T1, T2, T3>(T3 value) =>
            new Or<T1, T2, T3>(2, default(T1), default(T2), value);
    }
}
