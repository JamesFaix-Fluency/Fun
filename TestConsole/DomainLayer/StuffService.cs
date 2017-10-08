using System;
using System.Threading.Tasks;
using Fun;
using TestApp.Core;
using TestApp.Model;
using TestApp.ExternalFileIOPackage;
using System.Configuration;

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

        public Task<Try<Stuff>> CreateStuff(Stuff stuff)
        {
            return stuff.AsTry()
                .Assert(s => s.Id == 0, () => new ValidationException($"{nameof(stuff.Id)} cannot be assigned by clients."))
                .Assert(s => s.Name != null, () => new ValidationException($"{nameof(stuff.Name)} cannot be null."))
                .Assert(s => s.Count >= 0, () => new ValidationException($"{nameof(stuff.Count)} cannot be negative."))
                .TryMapAsync(_repository.CreateStuff)
                .TryDoAsync(s => LogAsync($"Created stuff '{stuff.Name}'."));
        }

        public Task<Try<Unit>> DeleteStuff(int id)
        {
            return id.AsTry()
                .Assert(n => n >= 0, () => new ValidationException($"{nameof(id)} cannot be negative."))
                .TryMapAsync(_repository.GetStuff)
                .TryMapAsync(s => _repository.DeleteStuff(id)
                    .TryDoAsync(_ => LogAsync($"Deleted stuff `{s.Name}`.")));
        }

        public Task<Try<Stuff>> GetStuff(int id)
        {
            return id.AsTry()
                .Assert(n => n >= 0, () => new ValidationException($"{nameof(id)} cannot be negative."))
                .TryMapAsync(_repository.GetStuff);
        }

        public Task<Try<Stuff>> UpdateStuff(Stuff stuff)
        {
            return stuff.AsTry()
                .Assert(s => s.Id >= 0, () => new ValidationException($"{nameof(stuff.Id)} cannot be negative."))
                .Assert(s => s.Name.Length <= 100, () => new ValidationException($"{nameof(stuff.Name)}.{nameof(stuff.Name.Length)} cannot exceed 100 characters."))
                .Assert(s => s.Count >= 0, () => new ValidationException($"{nameof(stuff.Count)} cannot be negative."))
                .TryMapAsync(_repository.UpdateStuff)
                .TryDoAsync(s => LogAsync($"Updated stuff '{s.Name}'."));
        }

        private Task<Try<Unit>> LogAsync(string message)
        {
            var logPath = ConfigurationManager.AppSettings["LogFilePath"];
            ;
            return Try.GetAsync(() =>
                _fileSystem.AppendLineAsync(Session.CurrentUser, logPath, message));
        }
    }
}
