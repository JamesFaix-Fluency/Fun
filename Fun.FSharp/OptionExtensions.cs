using System;
using Microsoft.FSharp.Core;

namespace Fun.FSharp
{
    public static class OptionExtensions
    {
        public static Maybe<T> AsMaybe<T>(
            this FSharpOption<T> @this) =>
            FSharpOption<T>.get_IsSome(@this)
                ? Maybe.Some(@this.Value)
                : Maybe.None<T>();
    }
}
