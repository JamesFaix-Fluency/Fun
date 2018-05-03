using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fun.Files
{
    public interface IFileSystem
    {
        Result<IPath> CreatePath(params string[] elements);

        Task<Result<IEnumerable<IPath>>> GetPaths(PathQuery query);

        Task<Result<string>> ReadFile(IPath path);

        Task<Result<Unit>> WriteFile(IPath path, string text);

        Task<Result<Unit>> ApplyChange(FileChange change);

        Task<Result<Unit>> CleanFolder(PathQuery query);

        Task<Result<IPath>> GetApplicationFolder();

        Task<bool> Exists(IPath path);
    }
}
