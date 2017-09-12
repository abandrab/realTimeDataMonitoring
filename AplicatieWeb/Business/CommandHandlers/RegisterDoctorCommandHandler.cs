using System;
using Business.Commands;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using Vanguard;
using Common.Layer.Auth;
using Common.Layer;

namespace Business.CommandHandlers
{
    public class RegisterDoctorCommandHandler : ICommandHandler<RegisterDoctorCommand>
    {
        private readonly IDoctorRepository doctorRepository;
        public RegisterDoctorCommandHandler(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }
        public void Execute(RegisterDoctorCommand command)
        {
            Guard.ArgumentNotNull(command, nameof(command));

            var doctor = command.Doctor;
            doctor.UserDetails.Password = PasswordManager.HashPassword(command.Doctor.UserDetails.Password);
            doctor.UserDetails.Role = Roles.Doctor;
            doctor.UserDetails.FirstChange = true;

            this.doctorRepository.Add(doctor);
            this.doctorRepository.Save();

        }
    }
}
