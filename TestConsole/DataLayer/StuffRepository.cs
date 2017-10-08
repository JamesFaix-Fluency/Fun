using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Fun;
using Fun.Dapper;
using TestApp.Core;
using TestApp.Model;

namespace TestApp.DataLayer
{
    public class StuffRepository : IStuffRepository
    {
        public Task<Try<Stuff>> CreateStuff(Stuff stuff)
        {
            const string query = @"
                INSERT INTO Stuff ([Name], [Count])
                    (@Name, @Count)";

            var param = new
            {
                Name = stuff.Name,
                Count = stuff.Count
            };

            return Try
                .UsingAsync(OpenConnection, cn => cn.TryQuerySingleAsync<int>(query, param))
                .TryMapAsync(createdId => GetStuff(createdId));
        }

        public Task<Try<Unit>> DeleteStuff(int id)
        {
            const string query = @"
                DELETE FROM Stuff 
                WHERE Id = @Id";

            var param = new
            {
                Id = id
            };

            return Try
                .UsingAsync(OpenConnection, cn => cn.TryExecuteAsync(query, param))
                .IgnoreAsync();
        }

        public Task<Try<Stuff>> GetStuff(int id)
        {
            const string query = @"
                SELECT Id, [Name], [Count]
                FROM Stuff
                WHERE Id = @Id
                    AND IsDeleted = 0";

            var param = new
            {
                Id = id
            };

            return Try
                .UsingAsync(OpenConnection, cn => cn.TryQuerySingleAsync<dynamic>(query, param))
                .TryMapAsync(d => new Stuff
                {
                    Id = d.Id,
                    Name = d.Name,
                    Count = d.Count
                });
        }
        
        public Task<Try<IEnumerable<Stuff>>> GetAllStuffs()
        {
            const string query = @"
                SELECT Id, [Name], [Count]
                FROM Stuff
                WHERE IsDeleted = 0";
            
            return Try
                .UsingAsync(OpenConnection, cn => cn.TryQueryAsync<dynamic>(query))
                .TryMapEachAsync(d => new Stuff
                {
                    Id = d.Id,
                    Name = d.Name,
                    Count = d.Count
                });
        }

        public Task<Try<Stuff>> UpdateStuff(Stuff stuff)
        {
            const string query = @"
                UPDATE Stuff
                SET [Name] = @Name, 
                    [Count] = @Count
                WHERE Id = @Id";

            var param = new
            {
                Id = stuff.Id,
                Name = stuff.Name,
                Coutn = stuff.Count
            };

            return Try
                .UsingAsync(OpenConnection, cn => cn.TryExecuteAsync(query, param))
                .TryMapAsync(_ => GetStuff(stuff.Id));
        }
        
        private async Task<IDbConnection> OpenConnection()
        {
            var connStr = ConfigurationManager.ConnectionStrings["Connection1"].ConnectionString;
            var cn = new SqlConnection(connStr);
            await cn.OpenAsync();
            return cn;
        }
    }
}
