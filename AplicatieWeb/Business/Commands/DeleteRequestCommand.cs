using Common.Layer.CqrsCore;

namespace Business.Commands
{
    public class DeleteRequestCommand : ICommand
    {
        public DeleteRequestCommand(string email)
        {
            Email = email;
        }
        public string Email { get; private set; }
    }
}
