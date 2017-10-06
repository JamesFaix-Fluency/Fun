using System;
using System.IO;
using System.Data.SqlClient;
using static Fun.Result;
using Dapper;

namespace Fun.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = Try(() => File.ReadAllText("file.txt"))
                .ThrowIf(String.IsNullOrEmpty, () => new InvalidOperationException("Requires non-empty string."))
                .Map(text => text.ToUpper())
                .Do(text => Console.Write(text));


            var y = TryUsing(() => new SqlConnection("asdfas"),
                    cn => cn.QuerySingle<string>("SELECT * FROM Stuff"))
                .Catch(typeof(TimeoutException), ex => Some(""));
        }
    }
}
