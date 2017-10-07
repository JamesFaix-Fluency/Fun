namespace Fun
{
    internal interface IOr2Factory
    {
        Or<T1, T2> First<T1, T2>(T1 value);

        Or<T1, T2> Second<T1, T2>(T2 value);
    }

    internal interface IOr3Factory<T1, T2, T3>
    {
        Or<T1, T2, T3> First(T1 value);

        Or<T1, T2, T3> Second(T2 value);

        Or<T1, T2, T3> Third(T3 value);
    }
}
