using System;

namespace Fun
{
    internal class OptFactory 
        : IOr2Factory
    {
        private static readonly Type _unitType = typeof(Unit);

        public Or<T1, T2> First<T1, T2>(T1 value)
        {
#if DEBUG
            //This will never be true of Opt instances, so we don't need to check 
            //since this class is internal and other Or types are not using it.
            if (typeof(T2) != _unitType)
                throw new InvalidOperationException($"{typeof(Opt<T1>)} must extend {typeof(Or<T1, Unit>)}");
#endif
            return Opt.Some(value) as Or<T1, T2>;
        }

        public Or<T1, T2> Second<T1, T2>(T2 error)
        {
#if DEBUG
            //This will never be true of Opt instances, so we don't need to check 
            //since this class is internal and other Or types are not using it.
            if (typeof(T2) != _unitType)
                throw new InvalidOperationException($"{typeof(Opt<T1>)} must extend {typeof(Or<T1, Unit>)}");
#endif
            return Opt.None<T1>() as Or<T1, T2>;
        }
    }
}
