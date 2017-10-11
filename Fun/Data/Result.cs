using System;

namespace Fun
{
    /// <summary>
    /// An object that may contain a value or an error.
    /// </summary>
    /// <typeparam name="T">Type of possible value.</typeparam>
    public class result<T> : IEquatable<result<T>>
    {
        private readonly T _value;
        private readonly Exception _error;

        internal result(T value)
        {
            _value = value;
        }

        internal result(Exception error)
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
                    $"Cannot get {nameof(Value)} of {nameof(result<T>)} when {nameof(HasValue)} is false.");

        /// <summary>
        /// Gets the error if <c>HasValue == false</c>, otherwise throws exception.
        /// </summary>
        public Exception Error =>
            HasValue
                ? throw new InvalidOperationException(
                    $"Cannot get {nameof(Error)} of {nameof(result<T>)} when {nameof(HasValue)} is false.")
                : _error;

        public override string ToString() =>
            HasValue
                ? $"Value({_value})"
                : $"Error({_error})";

        #region Equality

        public bool Equals(result<T> other) =>
            !Equals(other, null)
            && EqualsInner(this, other);

        public override bool Equals(object obj) =>
            Equals(obj as result<T>);

        public override int GetHashCode() =>
            HasValue
                ? _value.GetHashCode()
                : 0;

        public static bool operator ==(result<T> a, result<T> b) =>
            Equals(a, null)
                ? Equals(b, null)
                : EqualsInner(a, b);

        public static bool operator !=(result<T> a, result<T> b) =>
           !(Equals(a, null)
                ? Equals(b, null)
                : EqualsInner(a, b));

        private static bool EqualsInner(result<T> a, result<T> b) =>
            a.HasValue
                ? b.HasValue && Equals(a._value, b._value)
                : !b.HasValue && Equals(a._error, b._error);

        #endregion
    }
}
