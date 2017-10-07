using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Fun.Dapper
{
    public static partial class IDbConnectionExtensions
    {
        public static Task<Try<int>> ExecuteAsync(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            try
            {
                return SqlMapper
                    .ExecuteAsync(
                        connection,
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType)
                    .GetResultAsync();
            }
            catch (Exception e)
            {
                return Try.Error<int>(e).AsTask();
            }
        }

        public static Task<Try<IDataReader>> ExecuteReaderAsync(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            try
            {
                return SqlMapper
                    .ExecuteReaderAsync(
                        connection,
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType)
                    .GetResultAsync();
            }
            catch (Exception e)
            {
                return Try.Error<IDataReader>(e).AsTask();
            }
        }

        public static Task<Try<T>> ExecuteScalarAsync<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            try
            {
                return SqlMapper
                    .ExecuteScalarAsync<T>(
                        connection,
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType)
                    .GetResultAsync();
            }
            catch (Exception e)
            {
                return Try.Error<T>(e).AsTask();
            }
        }

        public static Task<Try<IEnumerable<T>>> QueryAsync<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            try
            {
                return SqlMapper
                    .QueryAsync<T>(
                        connection,
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType)
                    .GetResultAsync();
            }
            catch (Exception e)
            {
                return Try.Error<IEnumerable<T>>(e).AsTask();
            }
        }

        public static Task<Try<T>> QueryFirstAsync<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            try
            {
                return SqlMapper
                    .QueryFirstAsync<T>(
                        connection,
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType)
                    .GetResultAsync();
            }
            catch (Exception e)
            {
                return Try.Error<T>(e).AsTask();
            }
        }

        public static Task<Try<T>> QueryFirstOrDefaultAsync<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            try
            {
                return SqlMapper
                    .QueryFirstOrDefaultAsync<T>(
                        connection,
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType)
                    .GetResultAsync();
            }
            catch (Exception e)
            {
                return Try.Error<T>(e).AsTask();
            }
        }

        public static Task<Try<T>> QuerySingleAsync<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            try
            {
                return SqlMapper
                    .QuerySingleAsync<T>(
                        connection,
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType)
                    .GetResultAsync();
            }
            catch (Exception e)
            {
                return Try.Error<T>(e).AsTask();
            }
        }
        
        public static Task<Try<T>> QuerySingleOrDefaultAsync<T>(
            this IDbConnection connection,
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            try
            {
                return SqlMapper
                    .QuerySingleOrDefaultAsync<T>(
                        connection,
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType)
                    .GetResultAsync();
            }
            catch (Exception e)
            {
                return Try.Error<T>(e).AsTask();
            }
        }
    }
}
