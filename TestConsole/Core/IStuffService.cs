using System.Threading.Tasks;
using Fun;
using TestApp.Model;

namespace TestApp.Core
{
    public interface IStuffService
    {
        Task<Result<Stuff>> CreateStuff(Stuff stuff);

        Task<Result<unit>> DeleteStuff(int id);

        Task<Result<Stuff>> GetStuff(int id);

        Task<Result<Stuff>> UpdateStuff(Stuff stuff);
    }
}
