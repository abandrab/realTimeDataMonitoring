using Common.Layer.CqrsCore;

namespace Business.Queries
{
    public class GetUserQuery : IQuery
    {
        public GetUserQuery(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }
        public string Email { get; private set; }
        public string Password { get; private set; }
    }
}
