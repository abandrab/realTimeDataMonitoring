using System;
using Business.Commands;
using Common.Layer.CqrsCore;
using Vanguard;
using Sql.Server.Access.Interfaces;
using System.Linq;
using Domain.Model.Models.SqlServer;
using Common.Layer.Constants;

namespace Business.CommandHandlers
{
    public class AddRequestCommandHandler : ICommandHandler<AddRequestCommand>
    {
        private readonly IRequestRepository requestRepository;
        private readonly IPatientRepository patientRepository;
        private readonly IDoctorRepository doctorRepository;
        public AddRequestCommandHandler(IRequestRepository requestRepository,
            IPatientRepository patientRepository, IDoctorRepository doctorRepository)
        {
            this.requestRepository = requestRepository;
            this.patientRepository = patientRepository;
            this.doctorRepository = doctorRepository;
        }

        public void Execute(AddRequestCommand command)
        {
            Guard.ArgumentNotNull(command, nameof(command));

            var patientId = patientRepository.GetBy(p => p.UserDetails.Email.Equals(command.PatientEmail)).FirstOrDefault().Id;

            if(patientId!= null)
            {
                var request = new Request();
                request.DoctorId = command.DoctorId;
                request.PatientId = patientId;
                request.State = RequestStates.Pending;
                requestRepository.Add(request);
                requestRepository.Save();
            }
        }
    }
}
