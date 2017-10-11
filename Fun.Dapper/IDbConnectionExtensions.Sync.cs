using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Fun.Dapper
{
    public static partial class IDbConnectionExtensions
    {
        public static Result<int> TryExecute(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Result.Get(() =>
                connection.Execute(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Result<IDataReader> TryExecuteReader(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Result.Get(() =>
                connection.ExecuteReader(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Result<T> TryExecuteScalar<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Result.Get(() =>
                connection.ExecuteScalar<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Result<IEnumerable<T>> TryQuery<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Result.Get(() =>
                connection.Query<T>(sql, param,
                    transaction, buffered, commandTimeout, commandType));
        }

        public static Result<T> TryQueryFirst<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Result.Get(() =>
                connection.QueryFirst<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Result<T> TryQueryFirstOrDefault<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Result.Get(() =>
                connection.QueryFirstOrDefault<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Result<T> TryQuerySingle<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Result.Get(() =>
                connection.QuerySingle<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Result<T> TryQuerySingleOrDefault<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Result.Get(() =>
                connection.QuerySingleOrDefault<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }
    }
}
