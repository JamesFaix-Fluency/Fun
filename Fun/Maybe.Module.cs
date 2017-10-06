namespace Fun
{
    public static class Maybe
    {
        #region Main generators

        public static Maybe<T> Some<T>(
            T value) =>
            new Maybe<T>(true, value);

        public static Maybe<T> None<T>() =>
            Maybe<T>.None;

        #endregion
    }
}
