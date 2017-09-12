using Common.Layer.CqrsCore;
using System;

namespace Business.Commands
{
    public class AddRequestCommand : ICommand
    {
        public AddRequestCommand(string id, string email)
        {
            DoctorId = new Guid(id);
            PatientEmail = email;
        }
        public Guid DoctorId { get; private set; }
        public string PatientEmail { get; private set; }
    }
}
