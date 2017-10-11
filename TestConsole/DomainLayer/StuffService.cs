using System.Configuration;
using System.Threading.Tasks;
using Fun;
using Fun.Extensions;
using TestApp.Core;
using TestApp.ExternalFileIOPackage;
using TestApp.Model;

namespace TestApp.DomainLayer
{
    public class StuffService : IStuffService
    {
        private readonly IStuffRepository _repository;
        private readonly FileSystem _fileSystem;

        public StuffService(IStuffRepository repository, FileSystem fileSystem)
        {
            _repository = repository;
            _fileSystem = fileSystem;
        }

        public Task<result<Stuff>> CreateStuff(Stuff stuff)
        {
            return stuff.AsTry()
                .Assert(s => s.Id == 0, () => new ValidationException($"{nameof(stuff.Id)} cannot be assigned by clients."))
                .Assert(s => s.Name != null, () => new ValidationException($"{nameof(stuff.Name)} cannot be null."))
                .Assert(s => s.Count >= 0, () => new ValidationException($"{nameof(stuff.Count)} cannot be negative."))
                .MapAsync(_repository.CreateStuff)
                .DoAsync(s => LogAsync($"Created stuff '{stuff.Name}'."));
        }

        public Task<result<unit>> DeleteStuff(int id)
        {
            return id.AsTry()
                .Assert(n => n >= 0, () => new ValidationException($"{nameof(id)} cannot be negative."))
                .MapAsync(_repository.GetStuff)
                .MapAsync(s => _repository.DeleteStuff(id)
                    .DoAsync(_ => LogAsync($"Deleted stuff `{s.Name}`.")));
        }

        public Task<result<Stuff>> GetStuff(int id)
        {
            return id.AsTry()
                .Assert(n => n >= 0, () => new ValidationException($"{nameof(id)} cannot be negative."))
                .MapAsync(_repository.GetStuff);
        }

        public Task<result<Stuff>> UpdateStuff(Stuff stuff)
        {
            return stuff.AsTry()
                .Assert(s => s.Id >= 0, () => new ValidationException($"{nameof(stuff.Id)} cannot be negative."))
                .Assert(s => s.Name.Length <= 100, () => new ValidationException($"{nameof(stuff.Name)}.{nameof(stuff.Name.Length)} cannot exceed 100 characters."))
                .Assert(s => s.Count >= 0, () => new ValidationException($"{nameof(stuff.Count)} cannot be negative."))
                .MapAsync(_repository.UpdateStuff)
                .DoAsync(s => LogAsync($"Updated stuff '{s.Name}'."));
        }

        private Task<result<unit>> LogAsync(string message)
        {
            var logPath = ConfigurationManager.AppSettings["LogFilePath"];
            ;
            return Result.GetAsync(() =>
                _fileSystem.AppendLineAsync(Session.CurrentUser, logPath, message));
        }
    }
}
