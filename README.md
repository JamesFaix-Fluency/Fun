# Fun

Fun is a .NET Standard library that takes functional programming concepts from langauges like Haskell and F#, and implements them in a way that feels natural to consume in a C# codebase, rather than designing from functional first principles.  

[![Build status](https://ci.appveyor.com/api/projects/status/1xxpe6bfmxf5x93a?svg=true)](https://ci.appveyor.com/project/JamesFaix/fun)

#### Can't you just `using Microsoft.FSharp.Core;`?

There is some overlap, but Fun is designed expressly to be practical to use from C#. Implications of this are things like:
 1. Essential types with short names `Opt` vs `FSharpOption` (which the F# compiler aliases as `option`).
 2. `Func` and `Action` parameters, rather than `FSharpFunction`.
 3. `null` checks everywhere.
 4. An exception handling monad `Result`, which allows monadic error-handling, but also catches exceptions to avoid having two error-handling systems in the same place.



## Types

### Unit
`Unit` is basically identical to `Microsoft.FSharp.Core.Unit`, but it seemed silly to import a library just for the simplest type that exists.  This allows you to fake using `void` as a generic type argument for generic methods.  In some cases the need for overloads of a function that accept `Func<X, Y>` and `Action<X>` can be avoided by treating the second case as `Func<X, Unit>`.  `Unit` is used throughout Fun instead of `void`, but some methods expose overloads that take either `Action` or `Func<Unit>` parameters for compatibility.

### Opt{T}
`Opt<T>` is very similar to `System.Nullable<T>` and basically identical `Microsoft.FSharp.Core.FSharpOption<T>`.  It is an object that can contain either a value of type `T` or nothing.  Unlike `Nullable<T>`, it can be used on reference types too, but it has no C# langauge support. It exposes the same interface as `Nullable<T>`: 

    class Opt<T> {
        public bool HasValue { get; }
        public T Value { get; }
    }
 
 The static `Opt` class contains extension and generator methods for `Opt<T>`.
#### Generators
 - `Some<T>(T value)` - Creates a new `Opt<T>` instance with the given value.
 - `None<T>()` - Returns the singleton `Opt<T>` instance with no value.

#### Operators
 - `Do<T>` _(Several overloads)_ - If the input `Opt<T>` has a value, executes the given function, then returns the input `Opt<T>`.
 - `Ignore<T>` - If the input `Opt<T>` has a value, returns `Some(Unit)`; otherwise returns `None<T>`.
 - `Map<T1, T2>` _(Several overloads)_ - If the input `Opt<T1>` has a value, maps that value using the given function; otherwise returns `None<T2>`.  Some overloads are equivalent to standard monad `Bind` and `Fmap` operations.

### Result{T}
`Result<T>` is another "OR" type, which can have a value or an error. It exposes an interface very similar to `Nullable<T>` and `Maybe<T>`:

    class Result<T> {
        public bool HasValue { get; }
        public T Value { get; }
        public Exception Error { get; }
    }
    
The static `Result` class contains extension and generator methods for `Result<T>`.
#### Generators
 - `Value(T value)` - Creates a new `Result<T>` instance with the given value.
 - `Error(Exception error)` - Creates a new `Result<T>` instance with the given error.
 
#### Operators 
 - `Assert<T>` _(Several overloads)_ - If the input `Result<T> has a value and a given predicate is true, maps returns input, otherwise returns an error generated from a given function.
 - `Catch<T>` _(Several overloads)_ - If the input `Result<T> has an error, maps that error using the given function; otherwise returns the input value.
 - `Do<T>` _(Several overloads)_ - If the input `Result<T>` has a value, executes the given function, then returns the input `Result<T>`.
 - `Get<T>` _(Several overloads)_ - Evaluated the given function in a `try`/`catch` block and returns either a `Value` of the result, or `Error` of the caught exception.
 - `Ignore<T>` - If the input `Result<T>` has a value, returns `Value(Unit)`; otherwise returns input error.
 - `Map<T1, T2>` _(Several overloads)_ - If the input `Result<T1>` has a value, maps that value using the given function; otherwise returns the input error.  Some overloads are equivalent to standard monad `Bind` and `Fmap` operations.
 - `Using<T, TDisposable>` _(Several overloads)_ - Gets a new `IDisposable` instance from a given generator function, then gets a value using the `IDisposable` and returns a `Value` if successful or `Error` if an exception is thrown.

## Extensions
### Fun.Extensions
The `Fun.Extensions` namespace in _Fun.dll_ exposes some extension methods for `Task<T>`, `IEnumerable<T>`, `Nullable<T>`, and `T` for converting between "wrapped" and "raw" `T` values.

### Fun.Linq
The `Fun.Linq` namespace in _Fun.dll_ exposes extension methods for `Opt<T>` and `Result<T>` so they can be used in LINQ query expressions to some degree.

### Fun.FSharp
The _Fun.FSharp_ library has extension methods for converting to and from types in the F# core library.

### Fun.Dapper
The _Fun.Dapper_ library is a wrapper for the _Dapper_ object-relational mapper library that allow results from SQL queries to be returned as `Result<T>` instances, with any data access errors caught and wrapped with `Result.Error`.  This is basically shorthand for using _Dapper_ with `Result.Get`.
