using Business.Commands;
using Common.Layer;
using Common.Layer.Auth;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using Vanguard;

namespace Business.CommandHandlers
{
    public class RegisterPatientCommandHandler : ICommandHandler<RegisterPatientCommand>
    {
        private readonly IPatientRepository patientRepository;
        public RegisterPatientCommandHandler(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }
        public void Execute(RegisterPatientCommand command)
        {
            Guard.ArgumentNotNull(command, nameof(command));

            var patient = command.Patient;
            patient.UserDetails.Password = PasswordManager.HashPassword(command.Patient.UserDetails.Password);
            patient.UserDetails.Role = Roles.Patient;
            patient.UserDetails.FirstChange = false;

            this.patientRepository.Add(patient);
            this.patientRepository.Save();
        }
    }
}
