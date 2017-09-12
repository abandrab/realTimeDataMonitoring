using Common.Layer.CqrsCore;
using System;

namespace Business.Commands
{
    public class RejectRequestCommand : ICommand
    {
        public RejectRequestCommand(string id)
        {
            PatientId = new Guid(id);
        }
        public Guid PatientId { get; private set; }
    }
}
