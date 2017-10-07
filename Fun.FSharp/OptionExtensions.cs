using System;
using Microsoft.FSharp.Core;

namespace Fun.FSharp
{
    public static class OptionExtensions
    {
        public static Opt<T> AsOpt<T>(
            this FSharpOption<T> @this) =>
            FSharpOption<T>.get_IsSome(@this)
                ? Opt.Some(@this.Value)
                : Opt.None<T>();
    }
}
