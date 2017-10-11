using System;

namespace Fun
{
    public static partial class Opt
    {
        public static Opt<unit> Ignore<T>(
            this Opt<T> @this)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return Some(unit.Value);
        }
    }
}
