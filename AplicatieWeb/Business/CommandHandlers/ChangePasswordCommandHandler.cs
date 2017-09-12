using Business.Commands;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using System.Linq;
using Vanguard;

namespace Business.CommandHandlers
{
    public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
    {
        private readonly IUserDetailsRepository userDetailsRepository;
        public ChangePasswordCommandHandler(IUserDetailsRepository UserDetailsRepository)
        {
            this.userDetailsRepository = UserDetailsRepository;
        }
        public void Execute(ChangePasswordCommand command)
        {
            Guard.ArgumentNotNull(command, nameof(command));

            var userDetails = this.userDetailsRepository.GetBy(p => p.Email == command.Email)
               .FirstOrDefault();
            if(userDetails !=null)
            {
                userDetails.Password = command.Password;
                userDetails.FirstChange = false;
                this.userDetailsRepository.Update(userDetails);
                this.userDetailsRepository.Save();
            }           
        }
    }
}
