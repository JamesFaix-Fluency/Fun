using System.Text.RegularExpressions;

namespace Fun.Files
{
    public class RegexReplaceTransform : ITextTransform
    {
        public string Find { get; }

        public string Replacement { get; }

        private readonly Regex _regex;

        public RegexReplaceTransform(string find, string replacement)
        {
            Find = find;
            Replacement = replacement;
            _regex = new Regex(find);
        }

        public string Apply(string text) => _regex.Replace(text, m => Replacement);
    }
}
