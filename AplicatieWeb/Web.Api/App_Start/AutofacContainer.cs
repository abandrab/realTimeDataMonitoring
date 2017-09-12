using Autofac;
using Sql.Server.Access;
using System.Web.Http;
using Autofac.Integration.WebApi;
using System.Reflection;
using Common.Layer;
using Business;
using Common.Layer.CqrsCore;
using Common.Layer.AutofacModules;
using MongoDb.Access;

namespace Web.Api.App_Start
{
    public class AutofacContainer
    {
        public ILifetimeScope ConfigureContainer(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof(SqlServerAccess).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(MongoDbAccess).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder.RegisterModule(new CqrsModule());
            builder.RegisterAssemblyTypes(typeof(CommonLayerAccess).Assembly).AsClosedTypesOf(typeof(ICommandHandler<>));
            builder.RegisterAssemblyTypes(typeof(BusinessLayerAccess).Assembly).AsClosedTypesOf(typeof(ICommandHandler<>));
            builder.RegisterAssemblyTypes(typeof(BusinessLayerAccess).Assembly).AsClosedTypesOf(typeof(IQueryHandler<,>));
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            return container;
        }
    }
}