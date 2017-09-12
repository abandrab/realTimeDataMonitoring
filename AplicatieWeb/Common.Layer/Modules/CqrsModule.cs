using Autofac;
using Common.Layer.CqrsCore;
using Vanguard;

namespace Common.Layer.AutofacModules
{
    public class CqrsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Guard.ArgumentNotNull(builder, nameof(builder));
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();
            builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>();
        }
    }
}
