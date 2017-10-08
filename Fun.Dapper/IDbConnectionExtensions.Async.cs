using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Fun.Dapper
{
    public static partial class IDbConnectionExtensions
    {
        public static Task<Try<int>> TryExecuteAsync(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Try.GetAsync(() =>
                connection.ExecuteAsync(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Task<Try<IDataReader>> TryExecuteReaderAsync(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Try.GetAsync(() =>
                connection.ExecuteReaderAsync(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Task<Try<T>> TryExecuteScalarAsync<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Try.GetAsync(() =>
                connection.ExecuteScalarAsync<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Task<Try<IEnumerable<T>>> TryQueryAsync<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Try.GetAsync(() =>
                connection.QueryAsync<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Task<Try<T>> TryQueryFirstAsync<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Try.GetAsync(() =>
                connection.QueryFirstAsync<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Task<Try<T>> TryQueryFirstOrDefaultAsync<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Try.GetAsync(() =>
                connection.QueryFirstOrDefaultAsync<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Task<Try<T>> TryQuerySingleAsync<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Try.GetAsync(() =>
                connection.QuerySingleAsync<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Task<Try<T>> TryQuerySingleOrDefaultAsync<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Try.GetAsync(() =>
                 connection.QuerySingleOrDefaultAsync<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }
    }
}
