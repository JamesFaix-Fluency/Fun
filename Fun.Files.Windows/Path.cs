using System.Collections.Generic;
using System.Linq;

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

        public override string ToString()
        {
            if (_elements[0].EndsWith(":"))
            {
                var tail = System.IO.Path.Combine(_elements.Skip(1).ToArray());
                return $"{_elements[0]}\\{tail}";
            }
            else
            {
                return System.IO.Path.Combine(_elements);
            }
        }
    }
}
