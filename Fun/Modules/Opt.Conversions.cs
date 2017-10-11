using System;
using System.Collections.Generic;
using System.Linq;

namespace Fun
{
    public static partial class Opt
    {
        public static Nullable<T> AsNullable<T>(
                 this opt<T> @this)
                 where T : struct
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return @this.HasValue
                ? @this.Value
                : default(T?);
        }

        public static IEnumerable<T> AsSingleOrEmptySeq<T>(
            this opt<T> @this)
            where T : struct
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return @this.HasValue
                ? Enumerable.Repeat(@this.Value, 1)
                : Enumerable.Empty<T>();
        }

        public static result<T> AsResult<T>(
            this opt<T> @this,
            Func<Exception> errorGenerator)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(errorGenerator, null))
                throw new ArgumentNullException(nameof(errorGenerator));

            if (@this.HasValue)
            {
                return Result.Value(@this.Value);
            }
            else
            {
                try
                {
                    return Result.Error<T>(errorGenerator());
                }
                catch (Exception e)
                {
                    return Result.Error<T>(e);
                }
            }
        }
    }
}
