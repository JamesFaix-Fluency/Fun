namespace Fun
{
    public static partial class Opt
    {
        /// <summary>
        /// If <paramref name="value"/> is not null, creates a new <see cref="opt{T}"/> with the given value;
        /// otherwise returns <see cref="None"/>.
        /// </summary>
        public static opt<T> Some<T>(T value) =>
            Equals(value, null)
                ? opt<T>.None
                : new opt<T>(value);

        /// <summary>
        /// Gets an <see cref="opt{T}"/> with no value.
        /// </summary>
        public static opt<T> None<T>() => opt<T>.None; //Singleton
    }
}
