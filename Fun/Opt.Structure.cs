using System;

namespace Fun
{
    public class Opt<T> :
        IEquatable<Opt<T>>
    {
        private readonly bool _hasValue;

        private readonly T _value;

        public bool HasValue => _hasValue;

        public T Value =>
            _hasValue
                ? _value
                : throw new InvalidOperationException(
                    $"Cannot get {nameof(Value)} of {nameof(Opt<T>)} when {nameof(HasValue)} is false.");

        internal Opt(
            bool hasValue, 
            T value)
        {
            _hasValue = hasValue;
            _value = value;
        }

        #region Equality

        public bool Equals(
            Opt<T> other)
        {
            if (Equals(other, null))
            {
                return false;
            }

            if (_hasValue)
            {
                return other._hasValue
                    && Equals(_value, other._value);
            }
            else
            {
                return !other._hasValue;
            }
        }

        public override bool Equals(
            object obj) =>
            Equals(obj as Opt<T>);

        public override int GetHashCode() =>
            _value?.GetHashCode() ?? 0;

        public static bool operator ==(
            Opt<T> a, 
            Opt<T> b) =>
            Equals(a, null)
                ? Equals(b, null)
                : a.Equals(b);

        public static bool operator !=(
            Opt<T> a, 
            Opt<T> b) =>
            Equals(a, null)
                ? !Equals(b, null)
                : !a.Equals(b);

        #endregion

        public override string ToString() =>
            _hasValue
                ? $"Just {_value}"
                : $"Nothing{{{typeof(T)}}}";

        //Only ever create one None per type
        internal static Opt<T> None { get; } = 
            new Opt<T>(false, default(T));
    }
}
