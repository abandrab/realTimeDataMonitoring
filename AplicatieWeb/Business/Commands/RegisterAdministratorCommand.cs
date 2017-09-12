using Common.Layer.CqrsCore;
using Domain.Model.Models;

namespace Business.Commands
{
    public class RegisterAdministratorCommand : ICommand
    {
        public RegisterAdministratorCommand(Administrator administrator)
        {
            this.Administrator = administrator;
        }
        public Administrator Administrator { get; private set; }
    }
}
