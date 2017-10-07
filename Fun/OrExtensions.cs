using System;

namespace Fun
{
    public static class OrExtensions
    {
        #region 2 Projections

        public static TOrOut Map1<TIn1, TOut1, T2, TOrIn, TOrOut>(this TOrIn @this, Func<TIn1, TOut1> projection)
            where TOrIn : Or<TIn1, T2>
            where TOrOut : Or<TOut1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            switch (@this.Option)
            {
                case 1:
                    return (TOrOut)@this.Factory.First<TOut1, T2>(projection(@this.Item1));
                case 2:
                    return (TOrOut)@this.Factory.Second<TOut1, T2>(@this.Item2);
                default:
                    throw new InvalidOperationException(Or.GetInvalidOptionErrorMessage(typeof(Or<TIn1, T2>), @this.Option));
            }
        }

        public static TOrOut Map2<T1, TIn2, TOut2, TOrIn, TOrOut>(this TOrIn @this, Func<TIn2, TOut2> projection)
            where TOrIn : Or<T1, TIn2>
            where TOrOut : Or<T1, TOut2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            switch (@this.Option)
            {
                case 1:
                    return (TOrOut)@this.Factory.First<T1, TOut2>(@this.Item1);
                case 2:
                    return (TOrOut)@this.Factory.Second<T1, TOut2>(projection(@this.Item2));
                default:
                    throw new InvalidOperationException(Or.GetInvalidOptionErrorMessage(typeof(Or<T1, TIn2>), @this.Option));
            }
        }

        public static TOrOut Map1<TIn1, TOut1, T2,TOrIn, TOrOut>(this TOrIn @this, Func<TIn1, TOrOut> projection)
            where TOrIn : Or<TIn1, T2>
            where TOrOut : Or<TOut1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            switch (@this.Option)
            {
                case 1:
                    return projection(@this.Item1);
                case 2:
                    return (TOrOut)@this.Factory.Second<TOut1, T2>(@this.Item2);
                default:
                    throw new InvalidOperationException(Or.GetInvalidOptionErrorMessage(typeof(Or<TIn1, T2>), @this.Option));
            }
        }

        public static TOrOut Map2<T1, TIn2, TOut2, TOrIn, TOrOut>(this TOrIn @this, Func<TIn2, TOrOut> projection)
            where TOrIn : Or<T1, TIn2>
            where TOrOut : Or<T1, TOut2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            switch (@this.Option)
            {
                case 1:
                    return (TOrOut)@this.Factory.First<T1, TOut2>(@this.Item1);
                case 2:
                    return projection(@this.Item2);
                default:
                    throw new InvalidOperationException(Or.GetInvalidOptionErrorMessage(typeof(Or<T1, TIn2>), @this.Option));
            }
        }

        public static TOrOut Map<TIn1, TOut1, TIn2, TOut2, TOrIn, TOrOut>(
            this TOrIn @this,
            Func<TIn1, TOrOut> firstProjection,
            Func<TIn2, TOrOut> secondProjection)
            where TOrIn : Or<TIn1, TIn2>
            where TOrOut : Or<TOut1, TOut2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(firstProjection, null))
                throw new ArgumentNullException(nameof(firstProjection));

            if (Equals(secondProjection, null))
                throw new ArgumentNullException(nameof(secondProjection));

            switch (@this.Option)
            {
                case 1:
                    return firstProjection(@this.Item1);
                case 2:
                    return secondProjection(@this.Item2);
                default:
                    throw new InvalidOperationException(Or.GetInvalidOptionErrorMessage(typeof(TOrOut), @this.Option));
            }
        }

        #endregion

        #region 3 Projections

        public static Or<TOut1, T2, T3> Map1<TIn1, TOut1, T2, T3>(this Or<TIn1, T2, T3> @this, Func<TIn1, TOut1> projection)
        {
            switch (@this.Option)
            {
                case 1:
                    return Or3.First<TOut1, T2, T3>(projection(@this.Item1));
                case 2:
                    return Or3.Second<TOut1, T2, T3>(@this.Item2);
                case 3:
                    return Or3.Third<TOut1, T2, T3>(@this.Item3);
                default:
                    throw new InvalidOperationException(Or.GetInvalidOptionErrorMessage(typeof(Or<TIn1, T2, T3>), @this.Option));
            }
        }

        public static Or<T1, TOut2, T3> Map2<T1, TIn2, TOut2, T3>(this Or<T1, TIn2, T3> @this, Func<TIn2, TOut2> projection)
        {
            switch (@this.Option)
            {
                case 1:
                    return Or3.First<T1, TOut2, T3>(@this.Item1);
                case 2:
                    return Or3.Second<T1, TOut2, T3>(projection(@this.Item2));
                case 3:
                    return Or3.Third<T1, TOut2, T3>(@this.Item3);
                default:
                    throw new InvalidOperationException(Or.GetInvalidOptionErrorMessage(typeof(Or<T1, TIn2, T3>), @this.Option));
            }
        }

        public static Or<T1, T2, TOut3> Map3<T1, T2, TIn3, TOut3>(this Or<T1, T2, TIn3> @this, Func<TIn3, TOut3> projection)
        {
            switch (@this.Option)
            {
                case 1:
                    return Or3.First<T1, T2, TOut3>(@this.Item1);
                case 2:
                    return Or3.Second<T1, T2, TOut3>(@this.Item2);
                case 3:
                    return Or3.Third<T1, T2, TOut3>(projection(@this.Item3));
                default:
                    throw new InvalidOperationException(Or.GetInvalidOptionErrorMessage(typeof(Or<T1, T2, TIn3>), @this.Option));
            }
        }

        #endregion

        #region 2 Side effects

        public static TOr Do1<T1, T2, TOr>(this TOr @this, Action action)
            where TOr : Or<T1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.Option == 1)
            {
                action();
            }

            return @this;
        }

        public static TOr Do1<T1, T2, TOr>(this TOr @this, Action<T1> action)
            where TOr : Or<T1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.Option == 1)
            {
                action(@this.Item1);
            }

            return @this;
        }

        public static TOr Do1<T1, T2, TOr>(this TOr @this, Func<Unit> action)
            where TOr : Or<T1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.Option == 1)
            {
                action();
            }

            return @this;
        }


        public static TOr Do1<T1, T2, TOr>(this TOr @this, Func<T1, Unit> action)
            where TOr : Or<T1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.Option == 1)
            {
                action(@this.Item1);
            }

            return @this;
        }
        
        public static TOr Do2<T1, T2, TOr>(this TOr @this, Action action)
            where TOr : Or<T1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.Option == 2)
            {
                action();
            }

            return @this;
        }

        public static TOr Do2<T1, T2, TOr>(this TOr @this, Action<T2> action)
            where TOr : Or<T1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.Option == 2)
            {
                action(@this.Item2);
            }

            return @this;
        }

        public static TOr Do2<T1, T2, TOr>(this TOr @this, Func<Unit> action)
            where TOr : Or<T1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.Option == 2)
            {
                action();
            }

            return @this;
        }

        public static TOr Do2<T1, T2, TOr>(this TOr @this, Func<T2, Unit> action)
            where TOr : Or<T1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.Option == 2)
            {
                action(@this.Item2);
            }

            return @this;
        }

        #endregion

        #region 3 Side effects

        #endregion
    }
}
