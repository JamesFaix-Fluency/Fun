using System;

namespace Fun
{
    public static partial class Opt
    {
        public static opt<unit> Ignore<T>(
            this opt<T> @this)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return Some(unit.Value);
        }
    }
}
