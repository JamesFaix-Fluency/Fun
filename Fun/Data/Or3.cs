using System;

namespace Fun
{
    public class Or<T1, T2, T3>
        : IEquatable<Or<T1, T2, T3>>
    {
        protected readonly int _tag;

        protected readonly T1 _item1;

        protected readonly T2 _item2;

        protected readonly T3 _item3;

        private static Or3Factory _factory = new Or3Factory();

        internal Or(int tag, T1 item1, T2 item2, T3 item3)
        {
            if (tag < 1 || tag > 3)
                throw new ArgumentOutOfRangeException(nameof(tag), GetInvalidTagErrorMessage(tag));

            _tag = tag;
            _item1 = item1;
            _item2 = item2;
            _item3 = item3;
        }
        
        public int Tag => _tag;

        public T1 Item1 =>
            _tag != 1
                ? throw new InvalidOperationException(GetInvalidItemErrorMessage(1))
                : _item1;

        public T2 Item2 =>
            _tag != 2
                ? throw new InvalidOperationException(GetInvalidItemErrorMessage(2))
                : _item2;

        public T3 Item3 =>
            _tag != 3
                ? throw new InvalidOperationException(GetInvalidItemErrorMessage(3))
                : _item3;

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
                    throw new InvalidOperationException(GetInvalidTagErrorMessage(_tag));
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
                    throw new InvalidOperationException(GetInvalidTagErrorMessage(_tag));
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
                    throw new InvalidOperationException(GetInvalidTagErrorMessage(_tag));
            }
        }

        private static string GetInvalidItemErrorMessage(int number) =>
            $"Cannot get Item{number} from {typeof(Or<,,>)} unless {nameof(Tag)} is {number}.";

        private static string GetInvalidTagErrorMessage(int number) =>
            $"{typeof(Or<,,>)} cannot have an {nameof(Tag)} of {number}.";
    }
}
