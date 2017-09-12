using Common.Layer.CqrsCore;

namespace Business.Queries
{
    public class GetEmailQuery : IQuery
    {
        public GetEmailQuery(string email)
        {
            this.Email = email;
        }
        public string Email { get; private set; }
    }
}
