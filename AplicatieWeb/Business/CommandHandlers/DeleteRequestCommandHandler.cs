using System;
using Business.Commands;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using System.Linq;
using Vanguard;

namespace Business.CommandHandlers
{
    public class DeleteRequestCommandHandler : ICommandHandler<DeleteRequestCommand>
    {
        private readonly IPatientRepository patientRepository;
        private readonly IRequestRepository requestRepository;

        public DeleteRequestCommandHandler(IPatientRepository patientRepository, IRequestRepository requestRepository)
        {
            this.patientRepository = patientRepository;
            this.requestRepository = requestRepository;
        }
        public void Execute(DeleteRequestCommand command)
        {
            Guard.ArgumentNotNull(command, nameof(command));
            var patient = patientRepository.GetBy(d => d.UserDetails.Email.Equals(command.Email)).FirstOrDefault();
            if (patient != null)
            {
                var request = requestRepository.GetById(patient.Id);
                if(request != null)
                {
                    requestRepository.Delete(request);
                    requestRepository.Save();
                } 
            }
        }
    }
}
