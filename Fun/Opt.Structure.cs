using System;

namespace Fun
{
    public class Opt<T> :
        Or<T, Unit>,
        IEquatable<Opt<T>>
    {
        public bool HasValue => _option == 1;

        public T Value =>
            HasValue
                ? _item1
                : throw new InvalidOperationException($"Cannot get {nameof(Value)} of {nameof(Opt<T>)} when {nameof(HasValue)} is false.");

        internal Opt(T value) 
            : base(1, value, Unit.Value)
        { }

        private Opt() 
            : base(2, default(T), Unit.Value)
        { }

        //Only ever create one None per type
        internal static Opt<T> None { get; } = new Opt<T>();

        public bool Equals(Opt<T> other) => base.Equals(other);

        public override string ToString() =>
            HasValue
                ? $"Just {_item1}"
                : $"Nothing{{{typeof(T)}}}";
    }
}
