using System;
using System.IO;
using System.Data.SqlClient;
using Fun;
using Dapper;
using TestApp.DataLayer;
using TestApp.DomainLayer;
using TestApp.ServiceLayer;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = Try.Get(() => File.ReadAllText("file.txt"))
                .ThrowIf(String.IsNullOrEmpty, () => new InvalidOperationException("Requires non-empty string."))
                .Map(text => text.ToUpper())
                .Do(text => Console.Write(text));


            var y = Try.Using(() => new SqlConnection("asdfas"),
                    cn => cn.QuerySingle<string>("SELECT * FROM Stuff"))
                .Catch(typeof(TimeoutException), ex => Try.Some(""));
        }

        private static void Compose()
        {
            var repo = new StuffRepository();
            var serv = new StuffService(repo);
            var ctrl = new StuffController(serv);
        }
    }
}
