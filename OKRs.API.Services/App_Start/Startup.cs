using Microsoft.Owin;
using Newtonsoft.Json;
using OKRs.API.Services.Infrastructure;
using Owin;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;
using Swashbuckle.Application;

[assembly: OwinStartup("ProductionConfiguration", typeof(OKRs.API.Services.Startup))]
namespace OKRs.API.Services
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            var container = BootStrapper.ConfigureIoC(config);

            // Configure Formatters
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            JsonConvert.DefaultSettings = () =>
            {
                return new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                };
            };

            // Configure
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            config.MapHttpAttributeRoutes();
            
            SetupSwagger(config);

           // appBuilder.Use<HeaderValidationMiddleware>();
            appBuilder.UseWebApi(config);
        }

        private void SetupSwagger(HttpConfiguration config)
        {
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "OKRs.API.Service");
                c.GroupActionsBy(apiDesc => "All");
                c.UseFullTypeNameInSchemaIds();
            }
            )
            .EnableSwaggerUi(c =>
            {
                c.DisableValidator();
            });
        }
    }
}