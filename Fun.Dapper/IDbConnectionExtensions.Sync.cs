using System.Collections.Generic;
using System.Data;

namespace Fun.Dapper
{
    public static partial class IDbConnectionExtensions
    {
        public static Try<int> TryExecute(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Try.Get(() =>
                connection.Execute(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Try<IDataReader> TryExecuteReader(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Try.Get(() =>
                connection.ExecuteReader(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Try<T> TryExecuteScalar<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Try.Get(() =>
                connection.ExecuteScalar<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Try<IEnumerable<T>> TryQuery<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Try.Get(() =>
                connection.Query<T>(sql, param,
                    transaction, buffered, commandTimeout, commandType));
        }

        public static Try<T> TryQueryFirst<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Try.Get(() =>
                connection.QueryFirst<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Try<T> TryQueryFirstOrDefault<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Try.Get(() =>
                connection.QueryFirstOrDefault<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Try<T> TryQuerySingle<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Try.Get(() =>
                connection.QuerySingle<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }

        public static Try<T> TryQuerySingleOrDefault<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return Try.Get(() =>
                connection.QuerySingleOrDefault<T>(sql, param,
                    transaction, commandTimeout, commandType));
        }
    }
}
