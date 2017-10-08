using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fun
{
    public static class OrExtensions
    {
        private static readonly Type _or2Type = typeof(Or<,>);

        private static readonly Type _or3Type = typeof(Or<,,>);

        #region 2 Projections

        public static TOrOut Map1<TIn1, TOut1, T2, TOrIn, TOrOut>(
            this TOrIn @this, 
            Func<TIn1, TOut1> projection)
            where TOrIn : Or<TIn1, T2>
            where TOrOut : Or<TOut1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            switch (@this.Tag)
            {
                case 1:
                    return (TOrOut)@this.Factory.First<TOut1, T2>(projection(@this.Item1));
                case 2:
                    return (TOrOut)@this.Factory.Second<TOut1, T2>(@this.Item2);
                default:
                    throw new InvalidOperationException(Or.GetInvalidTagErrorMessage(_or2Type, @this.Tag));
            }
        }

        public static TOrOut Map2<T1, TIn2, TOut2, TOrIn, TOrOut>(
            this TOrIn @this, 
            Func<TIn2, TOut2> projection)
            where TOrIn : Or<T1, TIn2>
            where TOrOut : Or<T1, TOut2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            switch (@this.Tag)
            {
                case 1:
                    return (TOrOut)@this.Factory.First<T1, TOut2>(@this.Item1);
                case 2:
                    return (TOrOut)@this.Factory.Second<T1, TOut2>(projection(@this.Item2));
                default:
                    throw new InvalidOperationException(Or.GetInvalidTagErrorMessage(_or2Type, @this.Tag));
            }
        }

        public static TOrOut Map1<TIn1, TOut1, T2,TOrIn, TOrOut>(
            this TOrIn @this, 
            Func<TIn1, TOrOut> projection)
            where TOrIn : Or<TIn1, T2>
            where TOrOut : Or<TOut1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            switch (@this.Tag)
            {
                case 1:
                    return projection(@this.Item1);
                case 2:
                    return (TOrOut)@this.Factory.Second<TOut1, T2>(@this.Item2);
                default:
                    throw new InvalidOperationException(Or.GetInvalidTagErrorMessage(_or2Type, @this.Tag));
            }
        }

        public static TOrOut Map2<T1, TIn2, TOut2, TOrIn, TOrOut>(
            this TOrIn @this, 
            Func<TIn2, TOrOut> projection)
            where TOrIn : Or<T1, TIn2>
            where TOrOut : Or<T1, TOut2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            switch (@this.Tag)
            {
                case 1:
                    return (TOrOut)@this.Factory.First<T1, TOut2>(@this.Item1);
                case 2:
                    return projection(@this.Item2);
                default:
                    throw new InvalidOperationException(Or.GetInvalidTagErrorMessage(_or2Type, @this.Tag));
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

            switch (@this.Tag)
            {
                case 1:
                    return firstProjection(@this.Item1);
                case 2:
                    return secondProjection(@this.Item2);
                default:
                    throw new InvalidOperationException(Or.GetInvalidTagErrorMessage(_or2Type, @this.Tag));
            }
        }

        #endregion

        #region 2 Projections Async

        public static async Task<TOrOut> Map1Async<TIn1, TOut1, T2, TOrIn, TOrOut>(
            this TOrIn @this, 
            Func<TIn1, Task<TOut1>> projection)
            where TOrIn : Or<TIn1, T2>
            where TOrOut : Or<TOut1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            switch (@this.Tag)
            {
                case 1:
                    return (TOrOut)@this.Factory.First<TOut1, T2>(await projection(@this.Item1));
                case 2:
                    return (TOrOut)@this.Factory.Second<TOut1, T2>(@this.Item2);
                default:
                    throw new InvalidOperationException(Or.GetInvalidTagErrorMessage(_or2Type, @this.Tag));
            }
        }

        public static async Task<TOrOut> Map2Async<T1, TIn2, TOut2, TOrIn, TOrOut>(
            this TOrIn @this, 
            Func<TIn2, Task<TOut2>> projection)
            where TOrIn : Or<T1, TIn2>
            where TOrOut : Or<T1, TOut2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            switch (@this.Tag)
            {
                case 1:
                    return (TOrOut)@this.Factory.First<T1, TOut2>(@this.Item1);
                case 2:
                    return (TOrOut)@this.Factory.Second<T1, TOut2>(await projection(@this.Item2));
                default:
                    throw new InvalidOperationException(Or.GetInvalidTagErrorMessage(_or2Type, @this.Tag));
            }
        }

        public static async Task<TOrOut> Map1Async<TIn1, TOut1, T2, TOrIn, TOrOut>(
            this TOrIn @this, 
            Func<TIn1, Task<TOrOut>> projection)
            where TOrIn : Or<TIn1, T2>
            where TOrOut : Or<TOut1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            switch (@this.Tag)
            {
                case 1:
                    return await projection(@this.Item1);
                case 2:
                    return (TOrOut)@this.Factory.Second<TOut1, T2>(@this.Item2);
                default:
                    throw new InvalidOperationException(Or.GetInvalidTagErrorMessage(_or2Type, @this.Tag));
            }
        }

        public static async Task<TOrOut> Map2Async<T1, TIn2, TOut2, TOrIn, TOrOut>(
            this TOrIn @this, 
            Func<TIn2, Task<TOrOut>> projection)
            where TOrIn : Or<T1, TIn2>
            where TOrOut : Or<T1, TOut2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            switch (@this.Tag)
            {
                case 1:
                    return (TOrOut)@this.Factory.First<T1, TOut2>(@this.Item1);
                case 2:
                    return await projection(@this.Item2);
                default:
                    throw new InvalidOperationException(Or.GetInvalidTagErrorMessage(_or2Type, @this.Tag));
            }
        }

        public static async Task<TOrOut> MapAsync<TIn1, TOut1, TIn2, TOut2, TOrIn, TOrOut>(
            this TOrIn @this,
            Func<TIn1, Task<TOrOut>> firstProjection,
            Func<TIn2, Task<TOrOut>> secondProjection)
            where TOrIn : Or<TIn1, TIn2>
            where TOrOut : Or<TOut1, TOut2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(firstProjection, null))
                throw new ArgumentNullException(nameof(firstProjection));

            if (Equals(secondProjection, null))
                throw new ArgumentNullException(nameof(secondProjection));

            switch (@this.Tag)
            {
                case 1:
                    return await firstProjection(@this.Item1);
                case 2:
                    return await secondProjection(@this.Item2);
                default:
                    throw new InvalidOperationException(Or.GetInvalidTagErrorMessage(_or2Type, @this.Tag));
            }
        }

        #endregion

        #region 2 Sequence projections

        public static TOrOut MapEach1<TIn1, TOut1, T2, TOrIn, TOrOut>(
            this TOrIn @this,
            Func<TIn1, TOut1> projection)
            where TOrIn : Or<IEnumerable<TIn1>, T2>
            where TOrOut : Or<IEnumerable<TOut1>, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            switch (@this.Tag)
            {
                case 1:
                    return (TOrOut) @this.Factory.First<IEnumerable<TOut1>, T2>(@this.Item1.Select(projection));
                case 2:
                    return (TOrOut) @this.Factory.Second<IEnumerable<TOut1>, T2>(@this.Item2);
                default:
                    throw new InvalidOperationException(Or.GetInvalidTagErrorMessage(_or2Type, @this.Tag));
            }
        }

        public static TOrOut MapEach2<T1, TIn2, TOut2, TOrIn, TOrOut>(
            this TOrIn @this,
            Func<TIn2, TOut2> projection)
            where TOrIn : Or<T1, IEnumerable<TIn2>>
            where TOrOut : Or<T1, IEnumerable<TOut2>>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(projection, null))
                throw new ArgumentNullException(nameof(projection));

            switch (@this.Tag)
            {
                case 1:
                    return (TOrOut)@this.Factory.First<T1, IEnumerable<TOut2>>(@this.Item1);
                case 2:
                    return (TOrOut)@this.Factory.Second<T1, IEnumerable<TOut2>>(@this.Item2.Select(projection));
                default:
                    throw new InvalidOperationException(Or.GetInvalidTagErrorMessage(_or2Type, @this.Tag));
            }
        }

        #endregion

        #region 3 Projections

        public static TOrOut Map1<TIn1, TOut1, T2, T3, TOrIn, TOrOut>(
            this TOrIn @this, 
            Func<TIn1, TOut1> projection)
            where TOrIn : Or<TIn1, T2, T3>
            where TOrOut : Or<TOut1, T2, T3>
        {
            switch (@this.Tag)
            {
                case 1:
                    return (TOrOut)@this.Factory.First<TOut1, T2, T3>(projection(@this.Item1));
                case 2:
                    return (TOrOut) @this.Factory.Second<TOut1, T2, T3>(@this.Item2);
                case 3:
                    return (TOrOut)@this.Factory.Third<TOut1, T2, T3>(@this.Item3);
                default:
                    throw new InvalidOperationException(Or.GetInvalidTagErrorMessage(_or3Type, @this.Tag));
            }
        }

        public static TOrOut Map2<T1, TIn2, TOut2, T3, TOrIn, TOrOut>(
            this TOrIn @this, 
            Func<TIn2, TOut2> projection)
            where TOrIn : Or<T1, TIn2, T3>
            where TOrOut : Or<T1, TOut2, T3>
        {
            switch (@this.Tag)
            {
                case 1:
                    return (TOrOut)@this.Factory.First<T1, TOut2, T3>(@this.Item1);
                case 2:
                    return (TOrOut)@this.Factory.Second<T1, TOut2, T3>(projection(@this.Item2));
                case 3:
                    return (TOrOut)@this.Factory.Third<T1, TOut2, T3>(@this.Item3);
                default:
                    throw new InvalidOperationException(Or.GetInvalidTagErrorMessage(_or3Type, @this.Tag));
            }
        }

        public static TOrOut Map3<T1, T2, TIn3, TOut3, TOrIn, TOrOut>(
            this TOrIn @this, 
            Func<TIn3, TOut3> projection)
            where TOrIn : Or<T1, T2, TIn3>
            where TOrOut : Or<T1, T2, TOut3>
        {
            switch (@this.Tag)
            {
                case 1:
                    return (TOrOut) @this.Factory.First<T1, T2, TOut3>(@this.Item1);
                case 2:
                    return (TOrOut) @this.Factory.Second<T1, T2, TOut3>(@this.Item2);
                case 3:
                    return (TOrOut) @this.Factory.Third<T1, T2, TOut3>(projection(@this.Item3));
                default:
                    throw new InvalidOperationException(Or.GetInvalidTagErrorMessage(_or3Type, @this.Tag));
            }
        }

        public static TOrOut Map1<TIn1, TOut1, T2, T3, TOrIn, TOrOut>(
            this TOrIn @this, 
            Func<TIn1, TOrOut> projection)
            where TOrIn : Or<TIn1, T2, T3>
            where TOrOut : Or<TOut1, T2, T3>
        {
            switch (@this.Tag)
            {
                case 1:
                    return projection(@this.Item1);
                case 2:
                    return (TOrOut)@this.Factory.Second<TOut1, T2, T3>(@this.Item2);
                case 3:
                    return (TOrOut)@this.Factory.Third<TOut1, T2, T3>(@this.Item3);
                default:
                    throw new InvalidOperationException(Or.GetInvalidTagErrorMessage(_or3Type, @this.Tag));
            }
        }

        public static TOrOut Map2<T1, TIn2, TOut2, T3, TOrIn, TOrOut>(
            this TOrIn @this, 
            Func<TIn2, TOrOut> projection)
            where TOrIn : Or<T1, TIn2, T3>
            where TOrOut : Or<T1, TOut2, T3>
        {
            switch (@this.Tag)
            {
                case 1:
                    return (TOrOut)@this.Factory.First<T1, TOut2, T3>(@this.Item1);
                case 2:
                    return projection(@this.Item2);
                case 3:
                    return (TOrOut)@this.Factory.Third<T1, TOut2, T3>(@this.Item3);
                default:
                    throw new InvalidOperationException(Or.GetInvalidTagErrorMessage(_or3Type, @this.Tag));
            }
        }

        public static TOrOut Map3<T1, T2, TIn3, TOut3, TOrIn, TOrOut>(
            this TOrIn @this, 
            Func<TIn3, TOrOut> projection)
            where TOrIn : Or<T1, T2, TIn3>
            where TOrOut : Or<T1, T2, TOut3>
        {
            switch (@this.Tag)
            {
                case 1:
                    return (TOrOut)@this.Factory.First<T1, T2, TOut3>(@this.Item1);
                case 2:
                    return (TOrOut)@this.Factory.Second<T1, T2, TOut3>(@this.Item2);
                case 3:
                    return projection(@this.Item3);
                default:
                    throw new InvalidOperationException(Or.GetInvalidTagErrorMessage(_or3Type, @this.Tag));
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

            if (@this.Tag == 1)
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

            if (@this.Tag == 1)
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

            if (@this.Tag == 1)
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

            if (@this.Tag == 1)
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

            if (@this.Tag == 2)
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

            if (@this.Tag == 2)
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

            if (@this.Tag == 2)
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

            if (@this.Tag == 2)
            {
                action(@this.Item2);
            }

            return @this;
        }

        #endregion

        #region 2 Side effects async

        public static async Task<TOr> Do1Async<T1, T2, TOr>(this TOr @this, Task task)
            where TOr : Or<T1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(task, null))
                throw new ArgumentNullException(nameof(task));

            if (@this.Tag == 1)
            {
                await task;
            }

            return @this;
        }

        public static async Task<TOr> Do1Async<T1, T2, TOr>(this TOr @this, Func<T1, Task> getTask)
            where TOr : Or<T1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(getTask, null))
                throw new ArgumentNullException(nameof(getTask));

            if (@this.Tag == 1)
            {
                await getTask(@this.Item1);
            }

            return @this;
        }

        public static async Task<TOr> Do1Async<T1, T2, TOr>(this TOr @this, Task<Unit> task)
            where TOr : Or<T1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(task, null))
                throw new ArgumentNullException(nameof(task));

            if (@this.Tag == 1)
            {
                await task;
            }

            return @this;
        }
        public static async Task<TOr> Do1Async<T1, T2, TOr>(this TOr @this, Func<Task<Unit>> getTask)
           where TOr : Or<T1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(getTask, null))
                throw new ArgumentNullException(nameof(getTask));

            if (@this.Tag == 1)
            {
                await getTask();
            }

            return @this;
        }

        public static async Task<TOr> Do1Async<T1, T2, TOr>(this TOr @this, Func<T1, Task<Unit>> getTask)
            where TOr : Or<T1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(getTask, null))
                throw new ArgumentNullException(nameof(getTask));

            if (@this.Tag == 1)
            {
                await getTask(@this.Item1);
            }

            return @this;
        }

        public static async Task<TOr> Do2Async<T1, T2, TOr>(this TOr @this, Task task)
            where TOr : Or<T1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(task, null))
                throw new ArgumentNullException(nameof(task));

            if (@this.Tag == 2)
            {
                await task;
            }

            return @this;
        }

        public static async Task<TOr> Do2Async<T1, T2, TOr>(this TOr @this, Func<T2, Task> getTask)
            where TOr : Or<T1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(getTask, null))
                throw new ArgumentNullException(nameof(getTask));

            if (@this.Tag == 2)
            {
                await getTask(@this.Item2);
            }

            return @this;
        }

        public static async Task<TOr> Do2Async<T1, T2, TOr>(this TOr @this, Task<Unit> task)
            where TOr : Or<T1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(task, null))
                throw new ArgumentNullException(nameof(task));

            if (@this.Tag == 2)
            {
                await task;
            }

            return @this;
        }

        public static async Task<TOr> Do2Async<T1, T2, TOr>(this TOr @this, Func<Task<Unit>> getTask)
            where TOr : Or<T1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(getTask, null))
                throw new ArgumentNullException(nameof(getTask));

            if (@this.Tag == 2)
            {
                await getTask();
            }

            return @this;
        }

        public static async Task<TOr> Do2Async<T1, T2, TOr>(this TOr @this, Func<T2, Task<Unit>> getTask)
            where TOr : Or<T1, T2>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(getTask, null))
                throw new ArgumentNullException(nameof(getTask));

            if (@this.Tag == 2)
            {
                await getTask(@this.Item2);
            }

            return @this;
        }

        #endregion

        #region 3 Side effects

        public static TOr Do1<T1, T2, T3, TOr>(this TOr @this, Action action)
            where TOr : Or<T1, T2, T3>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.Tag == 1)
            {
                action();
            }

            return @this;
        }

        public static TOr Do1<T1, T2, T3, TOr>(this TOr @this, Action<T1> action)
            where TOr : Or<T1, T2, T3>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.Tag == 1)
            {
                action(@this.Item1);
            }

            return @this;
        }

        public static TOr Do1<T1, T2, T3, TOr>(this TOr @this, Func<Unit> action)
            where TOr : Or<T1, T2, T3>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.Tag == 1)
            {
                action();
            }

            return @this;
        }

        public static TOr Do1<T1, T2, T3, TOr>(this TOr @this, Func<T1, Unit> action)
            where TOr : Or<T1, T2, T3>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.Tag == 1)
            {
                action(@this.Item1);
            }

            return @this;
        }

        public static TOr Do2<T1, T2, T3, TOr>(this TOr @this, Action action)
            where TOr : Or<T1, T2, T3>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.Tag == 2)
            {
                action();
            }

            return @this;
        }

        public static TOr Do2<T1, T2, T3, TOr>(this TOr @this, Action<T2> action)
            where TOr : Or<T1, T2, T3>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.Tag == 2)
            {
                action(@this.Item2);
            }

            return @this;
        }

        public static TOr Do2<T1, T2, T3, TOr>(this TOr @this, Func<Unit> action)
            where TOr : Or<T1, T2, T3>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.Tag == 2)
            {
                action();
            }

            return @this;
        }

        public static TOr Do2<T1, T2, T3, TOr>(this TOr @this, Func<T2, Unit> action)
            where TOr : Or<T1, T2, T3>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.Tag == 2)
            {
                action(@this.Item2);
            }

            return @this;
        }

        public static TOr Do3<T1, T2, T3, TOr>(this TOr @this, Action action)
            where TOr : Or<T1, T2, T3>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.Tag == 3)
            {
                action();
            }

            return @this;
        }

        public static TOr Do3<T1, T2, T3, TOr>(this TOr @this, Action<T3> action)
            where TOr : Or<T1, T2, T3>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.Tag == 3)
            {
                action(@this.Item3);
            }

            return @this;
        }

        public static TOr Do3<T1, T2, T3, TOr>(this TOr @this, Func<Unit> action)
            where TOr : Or<T1, T2, T3>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.Tag == 3)
            {
                action();
            }

            return @this;
        }

        public static TOr Do3<T1, T2, T3, TOr>(this TOr @this, Func<T3, Unit> action)
            where TOr : Or<T1, T2, T3>
        {
            if (Equals(@this, null))
                throw new ArgumentNullException(nameof(@this));

            if (Equals(action, null))
                throw new ArgumentNullException(nameof(action));

            if (@this.Tag == 3)
            {
                action(@this.Item3);
            }

            return @this;
        }

        #endregion
    }
}
