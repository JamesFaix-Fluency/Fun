using System.Collections.Generic;

namespace Fun.Files.Windows
{
    public class Path : IPath
    {
        private readonly string[] _elements;
        public IEnumerable<string> Elements => _elements;

        public PathType Type { get; }

        internal Path(PathType type, params string[] elements)
        {
            _elements = elements;
            Type = type;
        }

        public override string ToString() =>
            System.IO.Path.Combine(_elements);
    }
}
