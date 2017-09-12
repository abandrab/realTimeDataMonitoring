using System;
using Business.Commands;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using Vanguard;
using System.Linq;
using Common.Layer.Constants;

namespace Business.CommandHandlers
{
    public class ApproveRequestCommandHandler : ICommandHandler<ApproveRequestCommand>
    {
        private readonly IDoctorRepository doctortRepository;
        private readonly IPatientRepository patientRepository;
        private readonly IRequestRepository requestRepository;

        public ApproveRequestCommandHandler(IDoctorRepository doctortRepository,
             IPatientRepository patientRepository, IRequestRepository requestRepository)
        {
            this.doctortRepository = doctortRepository;
            this.patientRepository = patientRepository;
            this.requestRepository = requestRepository;
        }

        public void Execute(ApproveRequestCommand command)
        {
            Guard.ArgumentNotNull(command, nameof(command));

            var doctor = doctortRepository.GetBy(d => d.UserDetails.Email.Equals(command.DoctorEmail)).FirstOrDefault();
            var patient = patientRepository.GetById(command.PatientId);
            if(doctor != null && patient != null)
            {
                patient.DoctorId = doctor.Id;
                patientRepository.Update(patient);
                patientRepository.Save();
                var request = requestRepository.GetById(patient.Id);
                request.State = RequestStates.Approved;
                requestRepository.Update(request);
                requestRepository.Save();
            }
        }
    }
}
