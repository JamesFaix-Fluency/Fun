using System;
using System.IO;
using System.Security.Authentication;
using System.Threading.Tasks;
using TestApp.Model;

namespace TestApp.ExternalFileIOPackage
{
    public class FileSystem
    {
        private void Validate(User user, string path)
        {
            if (Equals(user, null))
                throw new ArgumentNullException(nameof(user));

            if (String.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            if (!user.IsAuthenticated)
                throw new AuthenticationException($"{user.Name} ain't got no authentication!");

            if (!File.Exists(path))
                throw new FileNotFoundException($"Ain't no '{path}' file!");
        }

        public FileInfo GetFile(User user, string path)
        {
            Validate(user, path);
            return new FileInfo(path);
        }

        public Task<FileInfo> GetFileAsync(User user, string path)
        {
            Validate(user, path);
            return Task.Run(() => new FileInfo(path));
        }

        public FileStream Read(User user, string path)
        {
            Validate(user, path);
            return File.OpenRead(path);
        }

        public string ReadAllText(User user, string path)
        {
            Validate(user, path);
            return File.ReadAllText(path);
        }

        public Task<string> ReadAllTextAsync(User user, string path)
        {
            Validate(user, path);
            return Task.Run(() => File.ReadAllText(path));
        }

        public void AppendLine(User user, string path, string text)
        {
            if (Equals(text, null))
                throw new ArgumentNullException(nameof(text));

            Validate(user, path);

            using (var stream = File.Open(path, FileMode.Append))
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine(text);
            }
        }

        public Task AppendLineAsync(User user, string path, string text)
        {
            if (Equals(text, null))
                throw new ArgumentNullException(nameof(text));

            Validate(user, path);

            return Task.Run(() =>
            {
                using (var stream = File.Open(path, FileMode.Append))
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(text);
                }
            });
        }
    }
}
