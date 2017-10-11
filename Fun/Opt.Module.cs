using System;
using System.Collections.Generic;
using System.Linq;

namespace Fun
{
    public static class Opt
    {
        #region Main generators

        /// <summary>
        /// If <paramref name="value"/> is not null, creates a new <see cref="Opt{T}"/> with the given value;
        /// otherwise returns <see cref="None"/>.
        /// </summary>
        public static Opt<T> Some<T>(T value) => 
            Equals(value , null)
                ? Opt<T>.None
                : new Opt<T>(value);

        /// <summary>
        /// Gets an <see cref="Opt{T}"/> with no value.
        /// </summary>
        public static Opt<T> None<T>() => Opt<T>.None; //Singleton

        #endregion

        #region Projections

        //Functor map
        public static Opt<T2> OptMap<T1, T2>(
            this Opt<T1> @this,
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
        public static Opt<T2> OptMap<T1, T2>(
            this Opt<T1> @this,
            Func<T1, Opt<T2>> projection)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            return @this.HasValue
                ? projection(@this.Value)
                : None<T2>();
        }

        #endregion

        #region Side effects

        public static Opt<T> OptDo<T>(
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

        public static Opt<T> OptDo<T>(
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

        public static Opt<T> OptDo<T>(
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

        public static Opt<T> OptDo<T>(
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

        public static Opt<Unit> Ignore<T>(
            this Opt<T> @this)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return Some(Unit.Value);
        }

        #endregion

        #region Conversions

        public static Nullable<T> AsNullable<T>(
            this Opt<T> @this)
            where T : struct
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return @this.HasValue
                ? @this.Value
                : default(T?);
        }

        public static IEnumerable<T> AsSingleOrEmptySeq<T>(
            this Opt<T> @this)
            where T : struct
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            return @this.HasValue
                ? Enumerable.Repeat(@this.Value, 1)
                : Enumerable.Empty<T>();
        }

        public static Try<T> AsTry<T>(
            this Opt<T> @this,
            Func<Exception> errorGenerator)
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(errorGenerator, null))
                throw new ArgumentNullException(nameof(errorGenerator));

            if (@this.HasValue)
            {
                return Try.Some(@this.Value);
            }
            else
            {
                try
                {
                    return Try.Error<T>(errorGenerator());
                }
                catch (Exception e)
                {
                    return Try.Error<T>(e);
                }
            }
        }

        #endregion
    }
}
