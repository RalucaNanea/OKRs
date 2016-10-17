using OKRs.API.Services.Interfaces;
using OKRs.API.Services.Services;
using OKRs.DataContract;
using SimpleInjector;
using System.Configuration;
using System.Web.Http;
using static OKRs.API.Services.Infrastructure.TransactionFactoryDelegates;

namespace OKRs.API.Services.Infrastructure
{
    public class BootStrapper
    {
        public static Container ConfigureIoC(HttpConfiguration configuration)
        {
            var container = new Container();

            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


            container.Register(() => new OKRsDataAccessFactory(() => new DataAccessLayer(connectionString)), Lifestyle.Singleton);


            container.Register<ITeamService, TeamService>(Lifestyle.Transient);
            container.Register<ITeamMemberService, TeamMemberService>(Lifestyle.Transient);
           // container.Register<IDataAccessLayer, DataAccessLayer>(Lifestyle.Transient);
           // container.Register(typeof(IDataAccessLayer), typeof(DataAccessLayer));

            container.RegisterWebApiControllers(configuration);
            container.Verify();

            return container;
        }
    }
}