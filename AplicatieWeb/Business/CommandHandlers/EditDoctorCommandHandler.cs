using Business.Commands;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using Vanguard;
using System.Linq;
using Domain.Model.Models;
using Common.Layer.Helpers;

namespace Business.CommandHandlers
{
    public class EditDoctorCommandHandler : ICommandHandler<EditDoctorCommand>
    {
        private readonly IDoctorRepository doctorRepository;
        public EditDoctorCommandHandler(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }
        public void Execute(EditDoctorCommand command)
        {
            Guard.ArgumentNotNull(command, nameof(command));
            var doctor = this.doctorRepository.GetBy(p => p.UserDetails.Email == command.Doctor.UserDetails.Email)
               .FirstOrDefault();
            var password = doctor.UserDetails.Password;
            command.Doctor.UserDetails.Password = password;
            PropertyCopier<Doctor>.Copy(command.Doctor, doctor);
            doctorRepository.Update(doctor);
            this.doctorRepository.Save();
        }
    }
}
