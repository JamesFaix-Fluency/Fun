using System;

namespace Fun
{
    public static partial class Result
    {
        public static Opt<T> AsOpt<T>(
               this Result<T> @this)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return @this.HasValue
                ? Opt.Some(@this.Value)
                : Opt.None<T>();
        }

        public static T Force<T>(
        this Result<T> @this)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return @this.HasValue
                ? @this.Value
                : throw @this.Error;
        }
    }
}