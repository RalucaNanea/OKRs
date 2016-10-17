using Dapper;
using OKRs.DataContract;
using System;
using System.Collections.Generic;
using System.Data;


namespace OKRs.DataContract
{
    public class DataAccessLayer : IDataAccessLayer
    {
        private readonly IConnectionProvider _connectionProvider;
        public DataAccessLayer(string connectionString)
        {
            _connectionProvider = new ConnectionProvider(connectionString);
        }

        public IEnumerable<T> Query<T>(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var connection = CreateAndOpenConnection())
            {
                return connection.Query<T>(sql,
                    param: parameters,
                    commandType: commandType);
            }
        }

        public IEnumerable<TFirst> Query<TFirst, TSecond>(string sql, Func<TFirst, TSecond, TFirst> mapFunction, string splitOn = "Id", object parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var connection = CreateAndOpenConnection())
            {
                return connection.Query<TFirst, TSecond, TFirst>(sql,
                    param: parameters,
                    commandType: commandType,
                    map: mapFunction,
                    splitOn: splitOn);
            }
        }

        public IEnumerable<TFirst> Query<TFirst, TSecond, TThird>(string sql, Func<TFirst, TSecond, TThird, TFirst> mapFunction, string splitOn = "Id", object parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var connection = CreateAndOpenConnection())
            {
                return connection.Query<TFirst, TSecond, TThird, TFirst>(sql,
                    param: parameters,
                    commandType: commandType,
                    map: mapFunction,
                    splitOn: splitOn);
            }
        }

        public void Execute(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var connection = CreateAndOpenConnection())
            {
                connection.Execute(sql,
                    param: parameters,
                    commandType: commandType);
            }
        }

        public T ExecuteScalar<T>(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var connection = CreateAndOpenConnection())
            {
                return connection.ExecuteScalar<T>(sql,
                    param: parameters,
                    commandType: commandType);
            }
        }

        private IDbConnection CreateAndOpenConnection()
        {
            var connection = _connectionProvider.CreateConnection();
            connection.Open();
            return connection;
        }
    }
}
