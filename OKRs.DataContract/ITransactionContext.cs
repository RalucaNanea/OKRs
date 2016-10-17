using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKRs.DataContract
{
    public interface ITransactionContext : IDisposable
    {
        void Commit();

        void Rollback();

        IEnumerable<T> Query<T>(string sql, object parameters = null);

        void Execute(string sql, object parameters = null);

        T ExecuteScalar<T>(string sql, object parameters = null);
    }
}
