using System.Collections.Generic;
using System.Threading.Tasks;
using Fun;
using TestApp.Model;

namespace TestApp.Core
{
    public interface IStuffRepository
    {
        Task<Result<Stuff>> CreateStuff(Stuff stuff);

        Task<Result<unit>> DeleteStuff(int id);

        Task<Result<Stuff>> GetStuff(int id);

        Task<Result<IEnumerable<Stuff>>> GetAllStuffs();

        Task<Result<Stuff>> UpdateStuff(Stuff stuff);
    }
}
