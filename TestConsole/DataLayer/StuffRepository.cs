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
        public Task<Result<Stuff>> CreateStuff(Stuff stuff)
        {
            const string query = @"
                INSERT INTO Stuff ([Name], [Count])
                    (@Name, @Count)";

            var param = new
            {
                Name = stuff.Name,
                Count = stuff.Count
            };

            return Result
                .UsingAsync(OpenConnection, cn => cn.TryQuerySingleAsync<int>(query, param))
                .MapAsync(createdId => GetStuff(createdId));
        }

        public Task<Result<unit>> DeleteStuff(int id)
        {
            const string query = @"
                DELETE FROM Stuff 
                WHERE Id = @Id";

            var param = new
            {
                Id = id
            };

            return Result
                .UsingAsync(OpenConnection, cn => cn.TryExecuteAsync(query, param))
                .IgnoreAsync();
        }

        public Task<Result<Stuff>> GetStuff(int id)
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

            return Result
                .UsingAsync(OpenConnection, cn => cn.TryQuerySingleAsync<dynamic>(query, param))
                .MapAsync(d => new Stuff
                {
                    Id = d.Id,
                    Name = d.Name,
                    Count = d.Count
                });
        }
        
        public Task<Result<IEnumerable<Stuff>>> GetAllStuffs()
        {
            const string query = @"
                SELECT Id, [Name], [Count]
                FROM Stuff
                WHERE IsDeleted = 0";
            
            return Result
                .UsingAsync(OpenConnection, cn => cn.TryQueryAsync<dynamic>(query))
                .MapEachAsync(d => new Stuff
                {
                    Id = d.Id,
                    Name = d.Name,
                    Count = d.Count
                });
        }

        public Task<Result<Stuff>> UpdateStuff(Stuff stuff)
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

            return Result
                .UsingAsync(OpenConnection, cn => cn.TryExecuteAsync(query, param))
                .MapAsync(_ => GetStuff(stuff.Id));
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
