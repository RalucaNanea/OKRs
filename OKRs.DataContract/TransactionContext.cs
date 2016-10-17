using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace OKRs.DataContract
{
    public class TransactionContext : ITransactionContext
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public TransactionContext(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");

            _connection = new SqlConnection(connectionString);

            try
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction();
            }
            catch
            {
                if (_connection != null)
                    _connection.Dispose();

                if (_transaction != null)
                    _transaction.Dispose();

                throw;
            }
        }


        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }


        public IEnumerable<T> Query<T>(string sql, object parameters = null)
        {
            return _connection.Query<T>(sql,
                param: parameters,
                transaction: _transaction,
                commandType: CommandType.StoredProcedure);
        }


        public void Execute(string sql, object parameters = null)
        {
            _connection.Execute(sql,
                param: parameters,
                transaction: _transaction,
                commandType: CommandType.StoredProcedure);
        }

        public T ExecuteScalar<T>(string sql, object parameters = null)
        {
            return _connection.ExecuteScalar<T>(sql,
                param: parameters,
                transaction: _transaction,
                commandType: CommandType.StoredProcedure);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    if (_connection != null)
                        _connection.Dispose();
                }
                catch { }

                try
                {
                    if (_transaction != null)
                        _transaction.Dispose();
                }
                catch { }
            }
        }
    }
}