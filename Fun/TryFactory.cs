using System;

namespace Fun
{
    internal class TryFactory 
        : IOr2Factory
    {
        private static readonly Type _exceptionType = typeof(Exception);

        public Or<T1, T2> First<T1, T2>(T1 value)
        {
            if (typeof(T2) != _exceptionType)
                throw new InvalidOperationException($"{typeof(Try<T1>)} must extend {typeof(Or<T1, Exception>)}");

            return Try.Some(value) as Or<T1, T2>;
        }

        public Or<T1, T2> Second<T1, T2>(T2 error)
        {
            if (typeof(T2) != _exceptionType)
                throw new InvalidOperationException($"{typeof(Try<T1>)} must extend {typeof(Or<T1, Exception>)}");

            return Try.Error<T1>(error as Exception) as Or<T1, T2>;
        }
    }
}
