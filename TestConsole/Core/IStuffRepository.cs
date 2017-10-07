using System.Collections.Generic;
using System.Threading.Tasks;
using Fun;
using TestApp.Model;

namespace TestApp.Core
{
    public interface IStuffRepository
    {
        Task<Try<Stuff>> CreateStuff(Stuff stuff);

        Task<Try<Unit>> DeleteStuff(int id);

        Task<Try<Stuff>> GetStuff(int id);

        Task<Try<IEnumerable<Stuff>>> GetAllStuffs();

        Task<Try<Stuff>> UpdateStuff(Stuff stuff);
    }
}
