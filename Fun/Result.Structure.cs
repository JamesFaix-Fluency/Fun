using System;

namespace Fun
{
    public class Result<T> : 
        IEquatable<Result<T>>
    {
        private readonly T _value;

        private readonly Exception _error;

        public bool HasValue =>
            !Equals(_error, null);

        public T Value =>
            HasValue
                ? _value
                : throw new InvalidOperationException(
                    $"Cannot get {nameof(Value)} of {nameof(Result<T>)} when {nameof(HasValue)} is false.");

        public Exception Error =>
            HasValue
                ? throw new InvalidOperationException(
                    $"Cannot get {nameof(Error)} of {nameof(Result<T>)} when {nameof(HasValue)} is false.")
                : _error;

        internal Result(T value, Exception error)
        {
            _value = value;
            _error = error;
        }

        #region Equality
        
        public bool Equals(
            Result<T> other)
        {
            if (Equals(other, null))
            {
                return false;
            }

            if (HasValue)
            {
                return other.HasValue
                    && Equals(_value, other._value);
            }
            else
            {
                return !other.HasValue
                    && Equals(_error, other._error);
            }
        }

        public override bool Equals(
            object obj) =>
            Equals(obj as Result<T>);

        public override int GetHashCode() =>
            HasValue
                ? _value?.GetHashCode() ?? 0
                : _error?.GetHashCode() ?? 0;

        public static bool operator ==(
            Result<T> a, 
            Result<T> b) =>
            Equals(a, null)
                ? Equals(b, null)
                : a.Equals(b);

        public static bool operator !=(
            Result<T> a, 
            Result<T> b) =>
            Equals(a, null)
                ? !Equals(b, null)
                : !a.Equals(b);

        #endregion

        public override string ToString() =>
            HasValue
                ? $"Value({_value})"
                : $"Error({_error})";
    }
}
