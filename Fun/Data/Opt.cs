﻿using System;

namespace Fun
{
    /// <summary>
    /// An object that may or may not have a value.
    /// </summary>
    /// <remarks>
    /// This is conceptually the same as <see cref="Microsoft.FSharp.Core.FSharpOption{T}"/>
    /// and very similar to <see cref="System.Nullable{T}"/>.
    /// </remarks>
    /// <typeparam name="T">Type of possible value</typeparam>
    public class Opt<T> : IEquatable<Opt<T>>
    {
        private readonly bool _hasValue;
        private readonly T _value;

        //Consumers must use the static Opt class to instantiate.
        internal Opt(T value)
        {
            _hasValue = true;
            _value = value;
        }

        //A singleton object is used for None, so additional instances cannot be instantiated.
        private Opt()
        { }
        
        /// <summary>
        /// Gets whether the instance contains a value or not.
        /// </summary>
        public bool HasValue => _hasValue;

        /// <summary>
        /// Gets the value if <c>HasValue == true</c>, otherwise throws exception.
        /// </summary>
        public T Value =>
            _hasValue
                ? _value
                : throw new InvalidOperationException($"Cannot get {nameof(Value)} of {nameof(Opt<T>)} when {nameof(HasValue)} is false.");

        /// <summary>
        /// Singleton instance per generic type.
        /// Internal so static <see cref="Opt"/> class must be used by consumers.
        /// </summary>
        internal static Opt<T> None { get; } = new Opt<T>();

        public override string ToString() =>
            _hasValue
                ? $"Just {_value}"
                : $"Nothing{{{typeof(T)}}}";

        #region Equality

        public bool Equals(Opt<T> other) =>
            !Equals(other, null)
            && EqualsInner(this, other);

        public override bool Equals(object obj) =>
            Equals(obj as Opt<T>);

        public override int GetHashCode() =>
            _hasValue
                ? _value.GetHashCode()
                : 0;

        public static bool operator == (Opt<T> a, Opt<T> b) =>
            Equals(a, null)
                ? Equals(b, null)
                : EqualsInner(a, b);

        public static bool operator != (Opt<T> a, Opt<T> b) =>
           !(Equals(a, null)
                ? Equals(b, null)
                : EqualsInner(a, b));

        private static bool EqualsInner(Opt<T> a, Opt<T> b) =>
            a._hasValue == b._hasValue
            && Equals(a._value, b._value);

        #endregion
    }
}
