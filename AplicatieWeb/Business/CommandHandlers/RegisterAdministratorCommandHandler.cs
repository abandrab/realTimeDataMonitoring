using System;
using Common.Layer.CqrsCore;
using Business.Commands;
using Vanguard;
using Sql.Server.Access.Interfaces;
using Common.Layer;
using Common.Layer.Auth;

namespace Business.CommandHandlers
{
    public class RegisterAdministratorCommandHandler : ICommandHandler<RegisterAdministratorCommand>
    {
        private readonly IAdministratorRepository administratorRepository;
        public RegisterAdministratorCommandHandler(IAdministratorRepository administratorRepository)
        {
            this.administratorRepository = administratorRepository;
        }
        public void Execute(RegisterAdministratorCommand command)
        {
            Guard.ArgumentNotNull(command, nameof(command));

            var admin = command.Administrator;
            admin.UserDetails.Password = PasswordManager.HashPassword(command.Administrator.UserDetails.Password);
            admin.UserDetails.Role = Roles.Administrator;
            admin.UserDetails.FirstChange = false;

            this.administratorRepository.Add(admin);
            this.administratorRepository.Save();
        }
    }
}
