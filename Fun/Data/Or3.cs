using System;

namespace Fun
{
    /// <summary>
    /// A discriminated union type with three possible tags.
    /// </summary>
    /// <typeparam name="T1">First possible type</typeparam>
    /// <typeparam name="T2">Second possible type</typeparam>
    /// <typeparam name="T3">Third possible type</typeparam>
    public class Or<T1, T2, T3>
        : IEquatable<Or<T1, T2, T3>>
    {
        protected readonly int _tag;

        protected readonly T1 _item1;

        protected readonly T2 _item2;

        protected readonly T3 _item3;

        private static IOr3Factory _factory = new Or3Factory();

        //Consumers must use the static Or class to instantiate
        internal Or(int tag, T1 item1, T2 item2, T3 item3)
        {
            if (tag < 1 || tag > 3)
                throw new ArgumentOutOfRangeException(nameof(tag), Or.GetInvalidTagErrorMessage(GetType(), tag));

            _tag = tag;
            _item1 = item1;
            _item2 = item2;
            _item3 = item3;
        }

        /// <summary>
        /// The number of which of the possible values is being used.
        /// </summary>
        public int Tag => _tag;

        /// <summary>
        /// Gets the value if <c>Tag == 1</c>, otherwise throws an exception.
        /// </summary>
        public T1 Item1 =>
            _tag != 1
                ? throw new InvalidOperationException(Or.GetInvalidItemErrorMessage(GetType(), 1))
                : _item1;

        /// <summary>
        /// Gets the value if <c>Tag == 2</c>, otherwise throws an exception.
        /// </summary>
        public T2 Item2 =>
            _tag != 2
                ? throw new InvalidOperationException(Or.GetInvalidItemErrorMessage(GetType(), 2))
                : _item2;

        /// <summary>
        /// Gets the value if <c>Tag == 3</c>, otherwise throws an exception.
        /// </summary>
        public T3 Item3 =>
            _tag != 3
                ? throw new InvalidOperationException(Or.GetInvalidItemErrorMessage(GetType(), 3))
                : _item3;

        /// <summary>
        /// Gets a factory object that can produce <see cref="Or{,,}"/> instances.
        /// For any type <c>TOr{,,}</c> derived from <see cref="Or{,,}"/> this property can be overridden to 
        /// provide a way to create a <c>TOr{,,}</c> instance from another <c>TOr{,,}</c> instance.
        /// </summary>
        /// <remarks>
        /// Basically this is a way to get around not being able to include parameterized constructors in generic constraints.
        /// This allows extension methods to be written for a generic type <c>TOr{,,} where TOr{,,} : Or{,,}</c>
        /// which means monadic workflows can be created for different derived types using the same extension methods,
        /// rather than defining separate extension methods per derived type, or having extension methods that just return the base class.
        /// </remarks>
        internal virtual IOr3Factory Factory => _factory;

        #region Equality

        public bool Equals(Or<T1, T2, T3> other)
        {
            if (Equals(other, null)
                || _tag != other._tag)
            {
                return false;
            }

            switch (_tag)
            {
                case 1:
                    return Equals(_item1, other._item1);
                case 2:
                    return Equals(_item2, other._item2);
                case 3:
                    return Equals(_item3, other._item3);
                default:
                    throw new InvalidOperationException(Or.GetInvalidTagErrorMessage(GetType(), _tag));
            }
        }

        public override bool Equals(object obj) =>
            Equals(obj as Or<T1, T2, T3>);

        public override int GetHashCode()
        {
            switch (_tag)
            {
                case 1:
                    return _item1.GetHashCode();
                case 2:
                    return _item2.GetHashCode();
                case 3:
                    return _item3.GetHashCode();
                default:
                    throw new InvalidOperationException(Or.GetInvalidTagErrorMessage(GetType(), _tag));
            }
        }

        public static bool operator ==(Or<T1, T2, T3> a, Or<T1, T2, T3> b) =>
            Equals(a, null)
                ? Equals(b, null)
                : a.Equals(b);

        public static bool operator !=(Or<T1, T2, T3> a, Or<T1, T2, T3> b) =>
            !(a == b);

        #endregion

        public override string ToString()
        {
            switch (_tag)
            {
                case 1:
                    return $"{_tag}({_item1})";
                case 2:
                    return $"{_tag}({_item2})";
                case 3:
                    return $"{_tag}({_item3})";
                default:
                    throw new InvalidOperationException(Or.GetInvalidTagErrorMessage(GetType(), _tag));
            }
        }
    }
}
