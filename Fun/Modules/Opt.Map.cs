using System;

namespace Fun
{
    public static partial class Opt
    {
        //Functor map
        public static opt<T2> Map<T1, T2>(
            this opt<T1> @this,
            Func<T1, T2> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            return @this.HasValue
                ? Some(projection(@this.Value))
                : None<T2>();
        }

        //Monad bind
        public static opt<T2> Map<T1, T2>(
            this opt<T1> @this,
            Func<T1, opt<T2>> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            return @this.HasValue
                ? projection(@this.Value)
                : None<T2>();
        }
    }
}
