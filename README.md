# Fun

Fun is a .NET Standard library that takes functional programming concepts from langauges like Haskell and F#, and implements them in a way that feels natural to consume in a C# codebase, rather than designing from functional first principles.  

#### Can't you just `using Microsoft.FSharp.Core;`?

There is some overlap, but Fun is designed expressly to be practical to use from C#. Implications of this are things like:
 1. Essential types with short names `Maybe` vs `FSharpOption` (which the F# compiler aliases as `option`).
 2. `Func` and `Action` parameters, rather than `FSharpFunction`.
 3. `null` checks everywhere.
 4. An exception handling monad `Result`, which allows monadic error-handling, but also catches exceptions to avoid having two error-handling systems in the same place.



### Types

#### Unit
`Unit` is basically identical to `Microsoft.FSharp.Core.Unit`, but it seemed silly to import a library just for the simplest type that exists.  This allows you to fake using `void` as a generic type argument for generic methods.  In some cases the need for overloads of a function that accept `Func<X, Y>` and `Action<X>` can be avoided by treating the second case as `Func<X, Unit>`.  `Unit` is used throughout Fun instead of `void`, but some methods expose overloads that take either `Action` or `Func<Unit>` parameters for compatibility.

#### Maybe{T}
`Maybe<T>` is very similar to `System.Nullable<T>` and basically identical `Microsoft.FSharp.Core.FSharpOption<T>`.  It is an object that can contain either a value of type `T` or nothing.  Unlike `Nullable<T>`, it can be used on reference types too, but it has no C# langauge support. It exposes the same interface as `Nullable<T>`: 

    class Maybe<T> {
        public bool HasValue { get; }
        public T Value { get; }
    }
 
 There is also a static `Maybe` class that has static methods for `Maybe<T>` and an extension method class for operations on `Maybe<T>`.
 
#### Result{T}
`Result<T>` is another "OR" type, which can have a value or an error. It exposes an interface very similar to `Nullable<T>` and `Maybe<T>`:

    class Result<T> {
        public bool HasValue { get; }
        public T Value { get; }
        public Exception Error { get; }
    }
    
There is also a static `Result` class that has static methods for `Result<T>` and an extension method class for operations on `Result<T>`.

#### Classics
There are also some extension methods for `Task`, `IEnumerable`, `Func`, and `T`.
