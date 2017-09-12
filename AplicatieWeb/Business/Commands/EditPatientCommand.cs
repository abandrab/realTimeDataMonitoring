using Common.Layer.CqrsCore;
using Domain.Model.Models;

namespace Business.Commands
{
    public class EditPatientCommand : ICommand
    {
        public EditPatientCommand(Patient patient)
        {
            this.Patient = patient;
        }
        public Patient Patient { get; private set; }
    }
}
