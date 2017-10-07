using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Fun.Dapper
{
    public static partial class IDbConnectionExtensions
    {
        public static Try<int> Execute(
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
                    .Execute(
                        connection,
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType)
                    .AsTry();
            }
            catch (Exception e)
            {
                return Try.Error<int>(e);
            }
        }
                
        public static Try<IDataReader> ExecuteReader(
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
                    .ExecuteReader(
                        connection,
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType)
                    .AsTry();
            }
            catch (Exception e)
            {
                return Try.Error<IDataReader>(e);
            }
        }
               
        public static Try<T> ExecuteScalar<T>(
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
                    .ExecuteScalar<T>(
                        connection,
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType)
                    .AsTry();
            }
            catch (Exception e)
            {
                return Try.Error<T>(e);
            }
        }
                
        public static Try<IEnumerable<T>> Query<T>(
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
                    .Query<T>(
                        connection, 
                        sql, 
                        param, 
                        transaction,
                        buffered,
                        commandTimeout,
                        commandType)
                    .AsTry();
            }
            catch (Exception e)
            {
                return Try.Error<IEnumerable<T>>(e);
            }
        }
                
        public static Try<T> QueryFirst<T>(
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
                    .QueryFirst<T>(
                        connection,
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType)
                    .AsTry();
            }
            catch (Exception e)
            {
                return Try.Error<T>(e);
            }
        }
                
        public static Try<T> QueryFirstOrDefault<T>(
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
                    .QueryFirstOrDefault<T>(
                        connection,
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType)
                    .AsTry();
            }
            catch (Exception e)
            {
                return Try.Error<T>(e);
            }
        }
                
        public static Try<T> QuerySingle<T>(
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
                    .QuerySingle<T>(
                        connection,
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType)
                    .AsTry();
            }
            catch (Exception e)
            {
                return Try.Error<T>(e);
            }
        }
                
        public static Try<T> QuerySingleOrDefault<T>(
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
                    .QuerySingleOrDefault<T>(
                        connection,
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType)
                    .AsTry();
            }
            catch (Exception e)
            {
                return Try.Error<T>(e);
            }
        }
    }
}
