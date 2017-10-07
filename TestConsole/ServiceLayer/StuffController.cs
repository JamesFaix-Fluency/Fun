using System.Threading.Tasks;
using TestApp.Core;
using TestApp.Model;

namespace TestApp.ServiceLayer
{
    public class StuffController : IStuffController
    {
        private readonly IStuffService _service;

        public StuffController(IStuffService service)
        {
            _service = service;
        }
        
        public Task<string> DeleteStuff(int id)
        {
            return _service.DeleteStuff(id)
                .JsonSerializeAsync();
        }
        
        public Task<string> GetStuff(int id)
        {
            return _service.GetStuff(id)
                .JsonSerializeAsync();
        }

        public Task<string> PatchStuff(Stuff stuff)
        {
            return _service.UpdateStuff(stuff)
                .JsonSerializeAsync();
        }

        public Task<string> PostStuff(Stuff stuff)
        {
            return _service.CreateStuff(stuff)
                .JsonSerializeAsync();
        }
    }
}
