using System;

namespace Fun
{
    public static class OrExtensions
    {
        #region 2 Projections

        public static TOrOut Map1<T1, T2, T3, TOrIn, TOrOut>(this TOrIn @this, Func<T1, T3> projection)
            where TOrIn : Or<T1, T2>
            where TOrOut : Or<T3, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));
            
            switch (@this.Option)
            {
                case 1:
                    return (TOrOut)@this.Factory.First(projection(@this.Item1));
                case 2:
                    return (TOrOut)@this.Factory.Second(@this.Item2);
                default:
                    throw new InvalidOperationException(Or.GetInvalidOptionErrorMessage(typeof(Or<T1, T2>), @this.Option));
            }
        }

        public static TOrOut Map2<T1, T2, T3, TOrIn, TOrOut>(this TOrIn @this, Func<T2, T3> projection)
            where TOrIn : Or<T1, T2>
            where TOrOut : Or<T1, T3>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));
            
            switch (@this.Option)
            {
                case 1:
                    return (TOrOut)@this.Factory.First<T1, T3>(@this.Item1);
                case 2:
                    return (TOrOut)@this.Factory.Second<T1, T3>(projection(@this.Item2));
                default:
                    throw new InvalidOperationException(Or.GetInvalidOptionErrorMessage(typeof(Or<T1, T2>), @this.Option));
            }
        }

        #endregion

        #region

        public static Or<T4, T2, T3> Map1<T1, T2, T3, T4>(this Or<T1, T2, T3> @this, Func<T1, T4> projection)
        {
            switch (@this.Option)
            {
                case 1:
                    return Or3.First<T4, T2, T3>(projection(@this.Item1));
                case 2:
                    return Or3.Second<T4, T2, T3>(@this.Item2);
                case 3:
                    return Or3.Third<T4, T2, T3>(@this.Item3);
                default:
                    throw new InvalidOperationException(Or.GetInvalidOptionErrorMessage(typeof(Or<T1, T2, T3>), @this.Option));
            }
        }

        public static Or<T1, T4, T3> Map2<T1, T2, T3, T4>(this Or<T1, T2, T3> @this, Func<T2, T4> projection)
        {
            switch (@this.Option)
            {
                case 1:
                    return Or3.First<T1, T4, T3>(@this.Item1);
                case 2:
                    return Or3.Second<T1, T4, T3>(projection(@this.Item2));
                case 3:
                    return Or3.Third<T1, T4, T3>(@this.Item3);
                default:
                    throw new InvalidOperationException(Or.GetInvalidOptionErrorMessage(typeof(Or<T1, T2, T3>), @this.Option));
            }
        }

        public static Or<T1, T2, T4> Map3<T1, T2, T3, T4>(this Or<T1, T2, T3> @this, Func<T3, T4> projection)
        {
            switch (@this.Option)
            {
                case 1:
                    return Or3.First<T1, T2, T4>(@this.Item1);
                case 2:
                    return Or3.Second<T1, T2, T4>(@this.Item2);
                case 3:
                    return Or3.Third<T1, T2, T4>(projection(@this.Item3));
                default:
                    throw new InvalidOperationException(Or.GetInvalidOptionErrorMessage(typeof(Or<T1, T2, T3>), @this.Option));
            }
        }

        #endregion
    }
}
