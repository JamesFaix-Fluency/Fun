using System;

namespace Fun
{
    public sealed class Unit 
        : IEquatable<Unit>
    {
        private Unit() { }

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
