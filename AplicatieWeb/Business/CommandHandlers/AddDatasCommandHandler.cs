using System;
using Business.Commands;
using Common.Layer.CqrsCore;
using MongoDb.Access.Interfaces;
using Vanguard;

namespace Business.CommandHandlers
{
    public class AddDatasCommandHandler : ICommandHandler<AddDatasCommand>
    {
        private readonly IDataRepository dataRepository;
        public AddDatasCommandHandler(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }
        public void Execute(AddDatasCommand command)
        {
            Guard.ArgumentNotNull(command, nameof(command));
            this.dataRepository.AddMany(command.Datas);
        }
    }
}
