using System;

namespace Fun.Extensions
{
    public static class ExceptionExtensions
    {
        public static Try<T> AsTry<T>(this Exception @this) =>
            Try.Error<T>(@this);
    }
}
