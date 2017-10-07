using System;

namespace Fun
{
    public class Or<T1, T2>
        : IEquatable<Or<T1, T2>>
    {
        protected readonly int _option;

        protected readonly T1 _item1;

        protected readonly T2 _item2;

        private static Or2Factory _factory = new Or2Factory();
        
        internal Or(int option, T1 item1, T2 item2)
        {
            if (option < 1 || option > 2)
                throw new ArgumentOutOfRangeException(nameof(option), Or.GetInvalidOptionErrorMessage(GetType(), option));

            _option = option;
            _item1 = item1;
            _item2 = item2;
        }

        public int Option => _option;

        public T1 Item1 =>
            _option != 1
                ? throw new InvalidOperationException(Or.GetInvalidItemErrorMessage(GetType(), 1))
                : _item1;

        public T2 Item2 =>
            _option != 2
                ? throw new InvalidOperationException(Or.GetInvalidItemErrorMessage(GetType(), 2))
                : _item2;
        
        internal virtual IOr2Factory Factory => _factory;

        #region Equality

        public bool Equals(Or<T1, T2> other)
        {
            if (Equals(other, null) 
                || _option != other._option)
            {
                return false;
            }

            switch (_option)
            {
                case 1:
                    return Equals(_item1, other._item1);
                case 2:
                    return Equals(_item2, other._item2);
                default:
                    throw new InvalidOperationException(Or.GetInvalidOptionErrorMessage(GetType(), _option));
            }
        }

        public override bool Equals(object obj) =>
            Equals(obj as Or<T1, T2>);

        public override int GetHashCode()
        {
            switch (_option)
            {
                case 1:
                    return _item1.GetHashCode();
                case 2:
                    return _item2.GetHashCode();
                default:
                    throw new InvalidOperationException(Or.GetInvalidOptionErrorMessage(GetType(), _option));
            }
        }

        public static bool operator ==(Or<T1, T2> a, Or<T1, T2> b) =>
            Equals(a, null)
                ? Equals(b, null)
                : a.Equals(b);

        public static bool operator !=(Or<T1, T2> a, Or<T1, T2> b) =>
            !(a == b);

        #endregion

        public override string ToString()
        {
            switch (_option)
            {
                case 1:
                    return $"{_option}({_item1})";
                case 2:
                    return $"{_option}({_item2})";
                default:
                    throw new InvalidOperationException(Or.GetInvalidOptionErrorMessage(GetType(), _option));
            }
        }
    }
}
