namespace Fun.Files
{
    public static class ObjectExtensions
    {
        public static TBase Upcast<TDerived, TBase>(this TDerived @this)
            where TDerived : TBase
            => @this;
    }
}
