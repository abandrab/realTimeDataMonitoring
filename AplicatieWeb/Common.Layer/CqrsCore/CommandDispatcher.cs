using Autofac;
using Vanguard;

namespace Common.Layer.CqrsCore
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext context;

        public CommandDispatcher(IComponentContext context)
        {
            Guard.ArgumentNotNull(context, nameof(context));
            this.context = context;
        }

        public void Dispatch<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            Guard.ArgumentNotNull(command, nameof(command));
            var commandHandler = context.Resolve<ICommandHandler<TCommand>>();
            commandHandler.Execute(command);
        }
    }
}