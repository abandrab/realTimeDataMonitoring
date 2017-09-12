using Common.Layer.CqrsCore;
using System;

namespace Business.Commands
{
    public class ApproveRequestCommand : ICommand
    {
        public ApproveRequestCommand(string email, string id)
        {
            DoctorEmail = email;
            PatientId = new Guid(id);
        }
        public string DoctorEmail { get; private set; }
        public Guid PatientId { get; private set; }
    }
}
