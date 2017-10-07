using System;

namespace Fun
{
    public class Try<T> : 
        Or<T, Exception>,
        IEquatable<Try<T>>
    {
        private static readonly TryFactory<T> _factory = new TryFactory<T>();

        internal Try(T value)
            : base(1, value, null)
        { }

        internal Try(Exception error)
            : base(2, default(T), error)
        { }
        
        public bool HasValue => _option == 1;

        public T Value =>
            HasValue
                ? _item1
                : throw new InvalidOperationException(
                    $"Cannot get {nameof(Value)} of {nameof(Try<T>)} when {nameof(HasValue)} is false.");

        public Exception Error =>
            HasValue
                ? throw new InvalidOperationException(
                    $"Cannot get {nameof(Error)} of {nameof(Try<T>)} when {nameof(HasValue)} is false.")
                : _item2;

        internal override IOr2Factory<T, Exception> Factory => _factory;

        public bool Equals(Try<T> other) =>
            base.Equals(other);

        public override string ToString() =>
            HasValue
                ? $"Value({_item1})"
                : $"Error({_item2})";
    }
}
