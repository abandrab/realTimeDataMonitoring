using Common.Layer.CqrsCore;
using Domain.Model.Models;

namespace Business.Commands
{
    public class EditDoctorCommand : ICommand
    {
        public EditDoctorCommand(Doctor doctor)
        {
            this.Doctor = doctor;
        }
        public Doctor Doctor { get; private set; }
    }
}
