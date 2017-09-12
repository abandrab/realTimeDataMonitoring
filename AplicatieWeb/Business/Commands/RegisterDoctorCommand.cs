using Common.Layer.CqrsCore;
using Domain.Model.Models;

namespace Business.Commands
{
    public class RegisterDoctorCommand : ICommand
    {
        public RegisterDoctorCommand(Doctor doctor)
        {
            this.Doctor = doctor;
        }
        public Doctor Doctor { get; private set; }
    }
}
