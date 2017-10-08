using System;

namespace Fun
{
    /// <summary>
    /// An object that may contain a value or an error.
    /// </summary>
    /// <typeparam name="T">Type of possible value.</typeparam>
    public class Try<T> : 
        Or<T, Exception>,
        IEquatable<Try<T>>
    {
        private static readonly IOr2Factory _factory = new TryFactory();

        internal Try(T value)
            : base(1, value, null)
        { }

        internal Try(Exception error)
            : base(2, default(T), error)
        { }

        /// <summary>
        /// Gets whether the instance contains a value or not.
        /// </summary>
        public bool HasValue => _tag == 1;

        /// <summary>
        /// Gets the value if <c>HasValue == true</c>, otherwise throws exception.
        /// </summary>
        public T Value =>
            HasValue
                ? _item1
                : throw new InvalidOperationException(
                    $"Cannot get {nameof(Value)} of {nameof(Try<T>)} when {nameof(HasValue)} is false.");

        /// <summary>
        /// Gets the error if <c>HasValue == false</c>, otherwise throws exception.
        /// </summary>
        public Exception Error =>
            HasValue
                ? throw new InvalidOperationException(
                    $"Cannot get {nameof(Error)} of {nameof(Try<T>)} when {nameof(HasValue)} is false.")
                : _item2;

        /// <summary>
        /// Gets a factory object that can produce <see cref="Or{,}"/> instances.
        /// For any type <c>TOr{,}</c> derived from <see cref="Or{,}"/> this property can be overridden to 
        /// provide a way to create a <c>TOr{,}</c> instance from another <c>TOr{,}</c> instance.
        /// </summary>
        /// <remarks>
        /// Basically this is a way to get around not being able to include parameterized constructors in generic constraints.
        /// This allows extension methods to be written for a generic type <c>TOr{,} where TOr{,} : Or{,}</c>
        /// which means monadic workflows can be created for different derived types using the same extension methods,
        /// rather than defining separate extension methods per derived type, or having extension methods that just return the base class.
        /// </remarks>
        internal override IOr2Factory Factory => _factory;

        public bool Equals(Try<T> other) =>
            base.Equals(other);

        public override string ToString() =>
            HasValue
                ? $"Value({_item1})"
                : $"Error({_item2})";
    }
}
