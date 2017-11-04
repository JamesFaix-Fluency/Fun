using System.Collections.Generic;

namespace Fun.Files
{
    public interface IPath
    {
        IEnumerable<string> Elements { get; }

        PathType Type { get; }

        string ToString();
    }
}
