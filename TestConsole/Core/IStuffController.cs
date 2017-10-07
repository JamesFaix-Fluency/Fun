using Fun;
using System.Threading.Tasks;
using TestApp.Model;

namespace TestApp.Core
{
    public interface IStuffController
    {
        Task<string> DeleteStuff(int id);

        Task<string> GetStuff(int id);

        Task<string> PatchStuff(Stuff stuff);

        Task<string> PostStuff(Stuff stuff);
    }
}
