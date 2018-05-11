using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fun.Extensions;
using Fun.Files;

namespace Fun.Windows.Files
{
    public class FileSystem : IFileSystem
    {
        private static Regex _pathSeparatorRegex = new Regex(@"[\\\/]");

        public Result<IPath> CreatePath(params string[] elements) =>
            Result.Assert(elements?.Count() > 0,
                    () => new ArgumentException(nameof(elements), "Path must have 1 or more elements."))
                .Map(_ =>
                {
                    elements = SplitElements(elements);
                    return new Path(GetPathType(elements), elements) as IPath;
                });

        public Task<Result<IEnumerable<IPath>>> GetPaths(PathQuery query) =>
            Result.TryAsync(() =>
            {
                var paths = new List<IPath>();
                if (query.TypeFilter.HasFlag(PathType.File))
                {
                    var files = Directory.GetFiles(query.RootPath.ToString(), query.PatternFilter, GetSearchOption(query.IncludeSubfolders));
                    paths.AddRange(files.Select(f => new Path(PathType.File, f)));
                }
                if (query.TypeFilter.HasFlag(PathType.Folder))
                {
                    var dirs = Directory.GetDirectories(query.RootPath.ToString(), query.PatternFilter, GetSearchOption(query.IncludeSubfolders));
                    paths.AddRange(dirs.Select(d => new Path(PathType.Folder, d)));
                }
                return paths.AsEnumerable();
            });

        public Task<Result<string>> ReadFile(IPath path) =>
            Result.TryAsync(() => File.ReadAllText(path.ToString()));

        public Task<Result<Unit>> WriteFile(IPath path, string text) =>
            Result.TryAsync(() => File.WriteAllText(path.ToString(), text));

        public Task<Result<Unit>> ApplyChange(FileChange change) =>
            ReadFile(change.Path)
            .MapAsync(text =>
            {
                foreach (var tr in change.Transforms)
                {
                    text = tr.Apply(text);
                }
                return WriteFile(change.Path, text);
            });

        public Task<Result<Unit>> CleanFolder(PathQuery query) =>
            Result.TryAsync(() =>
            {
                var dir = new DirectoryInfo(query.RootPath.ToString());

                if (query.TypeFilter.HasFlag(PathType.Folder))
                {
                    var dirs = dir.GetDirectories(query.PatternFilter);
                    foreach (var d in dirs)
                    {
                        d.Delete(recursive: query.IncludeSubfolders);
                    }
                }

                if (query.TypeFilter.HasFlag(PathType.File))
                {
                    var files = dir.GetFiles(query.PatternFilter, GetSearchOption(query.IncludeSubfolders));
                    foreach (var f in files)
                    {
                        f.Delete();
                    }
                }
            });

        public Task<Result<IPath>> GetApplicationFolder() =>
            Result.TryAsync(() =>
            {
                var assembly = Assembly.GetExecutingAssembly();
                var dir = System.IO.Path.GetDirectoryName(assembly.Location);
                return CreatePath(dir).AsTask();
            });

        public Task<bool> Exists(IPath path) =>
            Task.Run(() => 
                path.Type == PathType.File 
                    ? File.Exists(path.ToString()) 
                    : Directory.Exists(path.ToString()));

        public Task<Result<Unit>> CreateFolderIfMissing(IPath path) =>
            Result.TryAsync(() =>
            {
                if (path.Type == PathType.File)
                {
                    return new ArgumentException("Cannot create folder from file path.").AsError<Unit>().AsTask();
                }

                if (!Directory.Exists(path.ToString()))
                {
                    Directory.CreateDirectory(path.ToString());
                }

                return Unit.Value.AsResult().AsTask();
            });

        private static PathType GetPathType(string[] elements) =>
            System.IO.Path.HasExtension(elements.Last())
                ? PathType.File
                : PathType.Folder;

        private static SearchOption GetSearchOption(bool includeSubfolders) =>
            includeSubfolders
                ? SearchOption.AllDirectories
                : SearchOption.TopDirectoryOnly;

        private static string[] SplitElements(string[] elements) =>
            elements
                .SelectMany(e => _pathSeparatorRegex.Split(e))
                .Where(e => e.Length > 0)
                .ToArray();
    }
}
