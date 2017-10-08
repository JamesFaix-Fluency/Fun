using System;
using System.Data.SqlClient;
using System.IO;
using Dapper;
using Fun;
using TestApp.DataLayer;
using TestApp.DomainLayer;
using TestApp.ExternalFileIOPackage;
using TestApp.ServiceLayer;
using TestApp.Model;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = Try.Get(() => File.ReadAllText("file.txt"))
                .ThrowIf(String.IsNullOrEmpty, () => new InvalidOperationException("Requires non-empty string."))
                .TryMap(text => text.ToUpper())
                .TryDo(text => Console.Write(text));


            var y = Try.Using(() => new SqlConnection("asdfas"),
                    cn => cn.QuerySingle<string>("SELECT * FROM Stuff"))
                .Catch(typeof(TimeoutException), ex => Try.Some(""));
        }

        private static void Compose()
        {
            Session.CurrentUser = new User
            {
                Name = "Me",
                IsAuthenticated = true
            };

            var fileSys = new FileSystem();
            var repo = new StuffRepository();
            var serv = new StuffService(repo, fileSys);
            var ctrl = new StuffController(serv);
        }
    }
}
