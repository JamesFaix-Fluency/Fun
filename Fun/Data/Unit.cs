using System;

namespace Fun
{
    /// <summary>
    /// A type with only one possible value.  
    /// </summary>
    /// <remarks>
    /// This is conceptually the same as <see cref="Microsoft.FSharp.Core.Unit"/>.
    /// </remarks>
    public sealed class unit 
        : IEquatable<unit>
    {
        /*
         * Instances of Unit cannot actually be instantiated because it is a reference type, 
         * which means null is a possible value, so no other values can be allowed.
         */
        private unit() { }
        
        /// <summary>
        /// Gets an instance of <see cref="unit"/>. (The only possible value is <c>null</c>.)
        /// </summary>
        public static unit Value => default(unit);

        public override string ToString() => "Unit";

        #region Equality

        public bool Equals(unit other) => true;

        public override bool Equals(object obj) => obj is unit;

        public override int GetHashCode() => 0;

        public static bool operator == (unit a, unit b) => true;

        public static bool operator != (unit a, unit b) => false;

        #endregion
    }   
}
