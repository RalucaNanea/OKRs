using System;
using System.Data;
using System.Data.SqlClient;

namespace OKRs.DataContract
{
    public class ConnectionProvider : IConnectionProvider
    {
        private readonly string _connectionString;

        public ConnectionProvider(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("connectionString");
            }

            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            return connection;
        }
    }
}