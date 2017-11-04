namespace Fun.Files
{
    public class PathQuery
    {
        public IPath RootPath { get; }

        public bool IncludeSubfolders { get; }

        public PathType TypeFilter { get; }

        public string PatternFilter { get; }

        public PathQuery(
            IPath rootPath,
            string patternFilter,
            PathType typeFilter,
            bool includeSubfolders)
        {
            RootPath = rootPath;
            PatternFilter = patternFilter;
            TypeFilter = typeFilter;
            IncludeSubfolders = includeSubfolders;
        }
    }
}
