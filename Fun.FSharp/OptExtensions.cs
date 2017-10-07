using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.FSharp.Core;

namespace Fun.FSharp
{
    public static class OptExtensions
    {
        public static FSharpOption<T> AsOption<T>(
            this Opt<T> @this) =>
            @this.HasValue
                ? FSharpOption<T>.Some(@this.Value)
                : FSharpOption<T>.None;
    }
}
