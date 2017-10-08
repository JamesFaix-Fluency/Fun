namespace Fun
{
    public static class Opt
    {
        #region Main generators

        /// <summary>
        /// Creates a new <see cref="Opt{T}"/> with the given value.
        /// </summary>
        public static Opt<T> Some<T>(T value) => new Opt<T>(value);

        /// <summary>
        /// Gets an <see cref="Opt{T}"/> with no value.
        /// </summary>
        public static Opt<T> None<T>() => Opt<T>.None; //Singleton

        #endregion
    }
}
