using System;

namespace Fun
{
    public static partial class Opt
    {
        public static opt<T> Do<T>(
            this opt<T> @this,
            Func<unit> action)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.HasValue)
            {
                action();
            }
            return @this;
        }

        public static opt<T> Do<T>(
            this opt<T> @this,
            Func<T, unit> action)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.HasValue)
            {
                action(@this.Value);
            }
            return @this;
        }

        public static opt<T> Do<T>(
            this opt<T> @this,
            Action action)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.HasValue)
            {
                action();
            }
            return @this;
        }

        public static opt<T> Do<T>(
            this opt<T> @this,
            Action<T> action)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.HasValue)
            {
                action(@this.Value);
            }
            return @this;
        }
    }
}
