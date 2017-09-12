using System.Web.Http;
using Web.Api.App_Start;

namespace Web.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
        //    GlobalConfiguration.Configure(configuration => WebApiConfig.Register(configuration));
        //    GlobalConfiguration.Configure(AutofacContainer.ConfigureContainer);
            // GlobalConfiguration.Configure(AutoMapperConfiguration.Configure);
           // GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
           // GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            // GlobalConfiguration.Configuration.Filters.Add(new ModelStateFilter());
            // FluentValidationConfiguration.Initialize(GlobalConfiguration.Configuration);
            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration>());
        }
    }
}
