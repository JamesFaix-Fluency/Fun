using System.Collections.Generic;
using System.Threading.Tasks;
using Fun;
using TestApp.Model;

namespace TestApp.Core
{
    public interface IStuffRepository
    {
        Task<result<Stuff>> CreateStuff(Stuff stuff);

        Task<result<unit>> DeleteStuff(int id);

        Task<result<Stuff>> GetStuff(int id);

        Task<result<IEnumerable<Stuff>>> GetAllStuffs();

        Task<result<Stuff>> UpdateStuff(Stuff stuff);
    }
}
