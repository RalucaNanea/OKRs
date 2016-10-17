using System.Data;

namespace OKRs.DataContract
{
    public interface IConnectionProvider
    {
        IDbConnection CreateConnection();
    }
}
