using Common.Layer.Auth;
using Common.Layer.CqrsCore;

namespace Business.Commands
{
    public class ChangePasswordCommand : ICommand
    {
        public ChangePasswordCommand(string email, string password)
        {
            this.Email = email;
            this.Password = PasswordManager.HashPassword(password);
        }
        public string Email { get; private set; }
        public string Password { get; private set; }
    }
}
