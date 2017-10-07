using System;

namespace Fun
{
    /// <summary>
    /// A type with only one possible value.  
    /// </summary>
    /// <remarks>
    /// This is conceptually the same as <see cref="Microsoft.FSharp.Core.Unit"/>
    /// </remarks>
    public sealed class Unit 
        : IEquatable<Unit>
    {
        /*
         * Instances of Unit cannot actually be instantiated because it is a reference type, 
         * which means null is a possible value, so no other values can be allowed.
         */
        private Unit() { }

        /// <summary>
        /// Gets an instance of <see cref="Unit"/>. (The only possible value is <c>null</c>.)
        /// </summary>
        public static Unit Value => default(Unit);

        #region Equality

        public bool Equals(Unit other) => true;

        public override bool Equals(object obj) => obj is Unit;

        public override int GetHashCode() => 0;

        public static bool operator == (Unit a, Unit b) => true;

        public static bool operator != (Unit a, Unit b) => false;

        #endregion

        public override string ToString() => "Unit";
    }   
}
