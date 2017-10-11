using System;

namespace Fun
{
    /// <summary>
    /// An object that may contain a value or an error.
    /// </summary>
    /// <typeparam name="T">Type of possible value.</typeparam>
    public class Try<T> : IEquatable<Try<T>>
    {
        private readonly T _value;
        private readonly Exception _error;

        internal Try(T value)
        {
            _value = value;
        }

        internal Try(Exception error)
        {
            _error = error;
        }

        /// <summary>
        /// Gets whether the instance contains a value or not.
        /// </summary>
        public bool HasValue => _error == null;

        /// <summary>
        /// Gets the value if <c>HasValue == true</c>, otherwise throws exception.
        /// </summary>
        public T Value =>
            HasValue
                ? _value
                : throw new InvalidOperationException(
                    $"Cannot get {nameof(Value)} of {nameof(Try<T>)} when {nameof(HasValue)} is false.");

        /// <summary>
        /// Gets the error if <c>HasValue == false</c>, otherwise throws exception.
        /// </summary>
        public Exception Error =>
            HasValue
                ? throw new InvalidOperationException(
                    $"Cannot get {nameof(Error)} of {nameof(Try<T>)} when {nameof(HasValue)} is false.")
                : _error;

        public override string ToString() =>
            HasValue
                ? $"Value({_value})"
                : $"Error({_error})";

        #region Equality

        public bool Equals(Try<T> other) =>
            !Equals(other, null)
            && EqualsInner(this, other);

        public override bool Equals(object obj) =>
            Equals(obj as Try<T>);

        public override int GetHashCode() =>
            HasValue
                ? _value.GetHashCode()
                : 0;

        public static bool operator ==(Try<T> a, Try<T> b) =>
            Equals(a, null)
                ? Equals(b, null)
                : EqualsInner(a, b);

        public static bool operator !=(Try<T> a, Try<T> b) =>
           !(Equals(a, null)
                ? Equals(b, null)
                : EqualsInner(a, b));

        private static bool EqualsInner(Try<T> a, Try<T> b) =>
            a.HasValue
                ? b.HasValue && Equals(a._value, b._value)
                : !b.HasValue && Equals(a._error, b._error);

        #endregion
    }
}
