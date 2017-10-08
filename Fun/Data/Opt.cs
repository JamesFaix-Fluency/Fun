using System;

namespace Fun
{
    /// <summary>
    /// An object that may or may not have a value.
    /// </summary>
    /// <remarks>
    /// This is conceptually the same as <see cref="Microsoft.FSharp.Core.Option{T}"/>.
    /// </remarks>
    /// <typeparam name="T">Type of possible value</typeparam>
    public class Opt<T> :
        Or<T, Unit>,
        IEquatable<Opt<T>>
    {
        //Singleton instance of None per type
        private static readonly Opt<T> _none = new Opt<T>();

        private static readonly IOr2Factory _factory = new OptFactory();
        
        //Consumers must use the static Opt class to instantiate.
        internal Opt(T value)
            : base(1, value, Unit.Value)
        { }

        //A singleton object is used for None, so additional instances cannot be instantiated.
        private Opt()
            : base(2, default(T), Unit.Value)
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
                : throw new InvalidOperationException($"Cannot get {nameof(Value)} of {nameof(Opt<T>)} when {nameof(HasValue)} is false.");

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

        /// <summary>
        /// Singleton instance per generic type.
        /// Internal so static <see cref="Opt"/> class must be used by consumers.
        /// </summary>
        internal static Opt<T> None => _none;

        public bool Equals(Opt<T> other) => base.Equals(other);

        public override string ToString() =>
            HasValue
                ? $"Just {_item1}"
                : $"Nothing{{{typeof(T)}}}";
    }
}
