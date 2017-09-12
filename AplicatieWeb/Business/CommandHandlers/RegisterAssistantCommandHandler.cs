using System;
using Business.Commands;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using Vanguard;
using Common.Layer.Auth;
using Common.Layer;

namespace Business.CommandHandlers
{
    public class RegisterAssistantCommandHandler : ICommandHandler<RegisterAssistantCommand>
    {
        private readonly IAssistantRepository assistantRepository;
        public RegisterAssistantCommandHandler(IAssistantRepository assistantRepository)
        {
            this.assistantRepository = assistantRepository;
        }
        public void Execute(RegisterAssistantCommand command)
        {
            Guard.ArgumentNotNull(command, nameof(command));

            var assistant = command.Assistant;
            assistant.UserDetails.Password = PasswordManager.HashPassword(command.Assistant.UserDetails.Password);
            assistant.UserDetails.Role = Roles.Assistant;
            assistant.UserDetails.FirstChange = true;

            this.assistantRepository.Add(assistant);
            this.assistantRepository.Save();
        }
    }
}
