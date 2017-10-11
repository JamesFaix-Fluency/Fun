using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Fun.Dapper
{
    public static partial class IDbConnectionExtensions
    {
        public static Task<result<int>> TryExecuteAsync(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Result.GetAsync(() =>
                connection.ExecuteAsync(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Task<result<IDataReader>> TryExecuteReaderAsync(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Result.GetAsync(() =>
                connection.ExecuteReaderAsync(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Task<result<T>> TryExecuteScalarAsync<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Result.GetAsync(() =>
                connection.ExecuteScalarAsync<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Task<result<IEnumerable<T>>> TryQueryAsync<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Result.GetAsync(() =>
                connection.QueryAsync<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Task<result<T>> TryQueryFirstAsync<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Result.GetAsync(() =>
                connection.QueryFirstAsync<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Task<result<T>> TryQueryFirstOrDefaultAsync<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Result.GetAsync(() =>
                connection.QueryFirstOrDefaultAsync<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Task<result<T>> TryQuerySingleAsync<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Result.GetAsync(() =>
                connection.QuerySingleAsync<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Task<result<T>> TryQuerySingleOrDefaultAsync<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Result.GetAsync(() =>
                 connection.QuerySingleOrDefaultAsync<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }
    }
}
