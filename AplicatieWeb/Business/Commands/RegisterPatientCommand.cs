using Common.Layer.CqrsCore;
using Domain.Model.Models;

namespace Business.Commands
{
    public class RegisterPatientCommand : ICommand
    {
        public RegisterPatientCommand(Patient patient)
        {
            this.Patient = patient;
        }
        public Patient Patient { get; private set; }
    }
}
