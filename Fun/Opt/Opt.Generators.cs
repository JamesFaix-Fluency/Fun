namespace Fun
{
    public static partial class Opt
    {
        /// <summary>
        /// If <paramref name="value"/> is not null, creates a new <see cref="Opt{T}"/> with the given value;
        /// otherwise returns <see cref="None"/>.
        /// </summary>
        public static Opt<T> Some<T>(T value) =>
            Equals(value, null)
                ? Opt<T>.None
                : new Opt<T>(value);

        /// <summary>
        /// Gets an <see cref="Opt{T}"/> with no value.
        /// </summary>
        public static Opt<T> None<T>() => Opt<T>.None; //Singleton
    }
}
