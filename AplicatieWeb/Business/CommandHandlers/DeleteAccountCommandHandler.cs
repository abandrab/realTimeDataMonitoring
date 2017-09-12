using System;
using Business.Commands;
using Common.Layer.CqrsCore;
using Vanguard;
using Sql.Server.Access.Interfaces;
using System.Linq;

namespace Business.CommandHandlers
{
    public class DeleteAccountCommandHandler : ICommandHandler<DeleteAccountCommand>
    {
        private readonly IUserDetailsRepository userDetailsRepository;
        public DeleteAccountCommandHandler(IUserDetailsRepository userDetailsRepository)
        {
            this.userDetailsRepository = userDetailsRepository;
        }
        public void Execute(DeleteAccountCommand command)
        {
            Guard.ArgumentNotNull(command, nameof(command));

            var user = userDetailsRepository.GetBy(u => u.Email.Equals(command.Email)).FirstOrDefault();
            if(user != null)
            {
                userDetailsRepository.Delete(user);
                userDetailsRepository.Save();
            }
        }
    }
}
