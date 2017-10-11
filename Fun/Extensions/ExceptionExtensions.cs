using System;

namespace Fun.Extensions
{
    public static class ExceptionExtensions
    {
        public static result<T> AsTry<T>(this Exception @this) =>
            Result.Error<T>(@this);
    }
}
