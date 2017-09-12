using Common.Layer.CqrsCore;
using Domain.Model.Models;

namespace Business.Commands
{
    public class RegisterAssistantCommand : ICommand
    {
        public RegisterAssistantCommand(Assistant assistant)
        {
            this.Assistant = assistant;
        }
        public Assistant Assistant { get; private set; }
    }
}
