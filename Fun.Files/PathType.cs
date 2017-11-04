using System;

namespace Fun.Files
{
    [Flags]
    public enum PathType
    {
        File = 0x1,
        Folder = 0x2
    }
}
