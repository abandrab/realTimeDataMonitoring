using Business.Commands;
using Common.Layer.CqrsCore;
using Vanguard;
using Sql.Server.Access.Interfaces;
using System.Linq;
using Common.Layer.Helpers;
using Domain.Model.Models;
using System.Diagnostics;

namespace Business.CommandHandlers
{
    public class EditPatientCommandHandler : ICommandHandler<EditPatientCommand>
    {
        private readonly IPatientRepository patientRepository;
        public EditPatientCommandHandler(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }
        public void Execute(EditPatientCommand command)
        {
            Guard.ArgumentNotNull(command, nameof(command));
            var patient = this.patientRepository.GetBy(p => p.Id.Equals(command.Patient.Id))
                .FirstOrDefault();
            
            if(patient != null)
            {
                var password = patient.UserDetails.Password;
                command.Patient.UserDetails.Password = password;
                PropertyCopier<Patient>.Copy(command.Patient, patient);
                Debug.Write(patient);
                this.patientRepository.Update(patient);
                this.patientRepository.Save();
            }
        }
    }
}
