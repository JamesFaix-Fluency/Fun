using System.Threading.Tasks;
using Fun;

namespace TestApp.ServiceLayer
{
    internal static class TryExtensions
    {
        public static async Task<string> JsonSerializeAsync<T>(this Task<Try<T>> @this)
        {
            return JsonSerialize(await @this);
        }

        private static string JsonSerialize(object obj)
        {
            return "{ not really JSON }";
        }
    }
}
