using System;
using System.Collections.Generic;
using System.Data;

namespace OKRs.DataContract
{
    public interface IDataAccessLayer
    {
        IEnumerable<T> Query<T>(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure);

        IEnumerable<TFirst> Query<TFirst, TSecond>(string sql, Func<TFirst, TSecond, TFirst> mapFunction, string splitOn = "Id", object parameters = null, CommandType commandType = CommandType.StoredProcedure);

        IEnumerable<TFirst> Query<TFirst, TSecond, TThird>(string sql, Func<TFirst, TSecond, TThird, TFirst> mapFunction, string splitOn = "Id", object parameters = null, CommandType commandType = CommandType.StoredProcedure);

        void Execute(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure);

        T ExecuteScalar<T>(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure);
    }
}
