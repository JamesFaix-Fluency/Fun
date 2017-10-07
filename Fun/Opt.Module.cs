namespace Fun
{
    public static class Opt
    {
        #region Main generators

        public static Opt<T> Some<T>(
            T value) =>
            new Opt<T>(true, value);

        public static Opt<T> None<T>() =>
            Opt<T>.None;

        #endregion
    }
}
