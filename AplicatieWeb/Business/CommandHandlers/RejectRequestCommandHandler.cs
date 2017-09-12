using System;
using Business.Commands;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using Vanguard;
using Common.Layer.Constants;

namespace Business.CommandHandlers
{
    public class RejectRequestCommandHandler : ICommandHandler<RejectRequestCommand>
    {
        private readonly IRequestRepository requestRepository;
        public RejectRequestCommandHandler(IRequestRepository requestRepository)
        {
            this.requestRepository = requestRepository;
        }
        
        public void Execute(RejectRequestCommand command)
        {
            Guard.ArgumentNotNull(command, nameof(command));

            var request = requestRepository.GetById(command.PatientId);
            if(request != null)
            {
                request.State = RequestStates.Rejected;
                requestRepository.Update(request);
                requestRepository.Save();
            }
        }
    }
}
