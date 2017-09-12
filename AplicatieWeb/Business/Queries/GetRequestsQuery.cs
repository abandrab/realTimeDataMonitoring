using Common.Layer.CqrsCore;

namespace Business.Queries
{
    public class GetRequestsQuery : IQuery
    {
        public GetRequestsQuery(string email)
        {
            this.Email = email;
        }
        public string Email { get; private set; }
    }
}
