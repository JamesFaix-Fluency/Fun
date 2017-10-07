using System;
using System.Threading.Tasks;
using Fun;
using TestApp.Core;
using TestApp.Model;

namespace TestApp.DomainLayer
{
    public class StuffService : IStuffService
    {
        private readonly IStuffRepository _repository;

        public StuffService(IStuffRepository repository)
        {
            _repository = repository;
        }
        
        public Task<Try<Stuff>> CreateStuff(Stuff stuff)
        {
            return stuff.AsTry()
                .Assert(s => s.Id == 0, () => new ValidationException($"{nameof(stuff.Id)} cannot be assigned by clients."))
                .Assert(s => s.Name != null, () => new ValidationException($"{nameof(stuff.Name)} cannot be null."))
                .Assert(s => s.Count >= 0, () => new ValidationException($"{nameof(stuff.Count)} cannot be negative."))
                .MapAsync(_repository.CreateStuff);
        }
        
        public Task<Try<Unit>> DeleteStuff(int id)
        {    
            return id.AsTry()
                .Assert(n => n >= 0, () => new ValidationException($"{nameof(id)} cannot be negative."))
                .MapAsync(_repository.DeleteStuff);
        }
        
        public Task<Try<Stuff>> GetStuff(int id)
        {
            return id.AsTry()
                   .Assert(n => n >= 0, () => new ValidationException($"{nameof(id)} cannot be negative."))
                   .MapAsync(_repository.GetStuff);
        }

        public Task<Try<Stuff>> UpdateStuff(Stuff stuff)
        {
            return stuff.AsTry()
                   .Assert(s => s.Id >= 0, () => new ValidationException($"{nameof(stuff.Id)} cannot be negative."))
                   .Assert(s => s.Name.Length <= 100, () => new ValidationException($"{nameof(stuff.Name)}.{nameof(stuff.Name.Length)} cannot exceed 100 characters."))
                   .Assert(s => s.Count >= 0, () => new ValidationException($"{nameof(stuff.Count)} cannot be negative."))
                   .MapAsync(_repository.UpdateStuff);
        }
    }

}
