using System.Threading.Tasks;
using Fun;
using TestApp.Model;

namespace TestApp.Core
{
    public interface IStuffService
    {
        Task<Try<Stuff>> CreateStuff(Stuff stuff);

        Task<Try<Unit>> DeleteStuff(int id);

        Task<Try<Stuff>> GetStuff(int id);

        Task<Try<Stuff>> UpdateStuff(Stuff stuff);
    }
}
