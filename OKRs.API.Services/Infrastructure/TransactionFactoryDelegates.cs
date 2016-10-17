using OKRs.DataContract;
namespace OKRs.API.Services.Infrastructure
{
    public class TransactionFactoryDelegates
    {
        public delegate IDataAccessLayer OKRsDataAccessFactory();
    }
}