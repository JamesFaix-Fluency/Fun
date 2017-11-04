using System.Collections.Generic;
using System.Linq;

namespace Fun.Files
{
    public class FileChange
    {
        public IPath Path { get; }
        
        public IEnumerable<ITextTransform> Transforms { get; }

        public FileChange(
            IPath path, 
            IEnumerable<ITextTransform> transforms)
        {
            Path = path;
            Transforms = transforms.ToList();
        }
    }
}
