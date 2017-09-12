using Common.Layer.Auth;
using Common.Layer.CqrsCore;

namespace Business.Queries
{
    public class CheckPasswordQuery : IQuery
    {
        public CheckPasswordQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }
        public string Email { get; private set; }
        public string Password { get; private set; }
    }
}
