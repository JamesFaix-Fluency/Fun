using System;

namespace Fun.Extensions
{
    public static class ExceptionExtensions
    {
        public static Result<T> AsError<T>(this Exception @this) =>
            Result.Error<T>(@this);
    }
}
