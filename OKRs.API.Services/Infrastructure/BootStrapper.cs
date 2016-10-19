using OKRs.API.Services.Interfaces;
using OKRs.API.Services.Services;
using OKRs.DataContract;
using SimpleInjector;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ve.Framework.Log;
using Ve.Rome.ExceptionFilter;
using static OKRs.API.Services.Infrastructure.TransactionFactoryDelegates;

namespace OKRs.API.Services.Infrastructure
{
    public class BootStrapper
    {
        public static Container ConfigureIoC(HttpConfiguration configuration)
        {
            var container = new Container();

            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

         
            //register services
            container.Register<ITeamService, TeamService>(Lifestyle.Transient);
            container.Register<ITeamMemberService, TeamMemberService>(Lifestyle.Transient);

            // register CustomExceptionFilter
            container.Register<ILogProvider, Log4NetProvider>(Lifestyle.Singleton);
            container.Register(() => new OKRsDataAccessFactory(() => new DataAccessLayer(connectionString)), Lifestyle.Singleton);
            var handlerMap = new ExceptionHandlerMap();
            handlerMap.Add(typeof(System.ArgumentException), (ex) =>
            {
                var httpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponse.Content = new StringContent(ex.Message);
                return httpResponse;
            });
            container.Register(() => new CustomExceptionFilterAttribute(container.GetInstance<ILogProvider>(), handlerMap), Lifestyle.Singleton);

            container.RegisterWebApiControllers(configuration);
            container.Verify();

            return container;
        }
    }
}