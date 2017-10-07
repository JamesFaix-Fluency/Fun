using System;

namespace Fun
{
    internal class TryFactory<T> 
        : IOr2Factory<T, Exception>
    {
        public Or<T, Exception> First(T value) => Try.Some(value);

        public Or<T, Exception> Second(Exception error) => Try.Error<T>(error);
    }
}
