# Fun

Fun is a .NET Standard library that takes functional programming concepts from langauges like Haskell and F#, and implements them in a way that feels natural to consume in a C# codebase, rather than designing from functional first principles.  

#### Can't you just `using Microsoft.FSharp.Core;`?

There is some overlap, but Fun is designed expressly to be practical to use from C#. Implications of this are things like:
 1. Essential types with short names `Maybe` vs `FSharpOption` (which the F# compiler aliases as `option`).
 2. `Func` and `Action` parameters, rather than `FSharpFunction`.
 3. `null` checks everywhere.
 4. An exception handling monad `Result`, which allows monadic error-handling, but also catches exceptions to avoid having two error-handling systems in the same place.
