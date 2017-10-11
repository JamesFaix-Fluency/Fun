using System;

namespace Fun
{
    public static partial class Opt
    {
        public static Opt<T> Do<T>(
            this Opt<T> @this,
            Func<Unit> action)
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

        public static Opt<T> Do<T>(
            this Opt<T> @this,
            Func<T, Unit> action)
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

        public static Opt<T> Do<T>(
            this Opt<T> @this,
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

        public static Opt<T> Do<T>(
            this Opt<T> @this,
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
