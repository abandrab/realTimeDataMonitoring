using Common.Layer.CqrsCore;

namespace Business.Commands
{
    public class DeleteAccountCommand : ICommand
    {
        public DeleteAccountCommand(string email)
        {
            Email = email;
        }
        public string Email { get; private set; }
    }
}
