namespace Fun.Files
{
    public class TextReplaceTransform : ITextTransform
    {
        public string Find { get; }
        public string Replacement { get; }

        public TextReplaceTransform(string find, string replacement)
        {
            Find = find;
            Replacement = replacement;
        }

        public string Apply(string text) => text.Replace(Find, Replacement);

        public TextReplaceTransform Inverse => new TextReplaceTransform(Replacement, Find);
    }
}
