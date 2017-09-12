using Common.Layer.CqrsCore;

namespace Business.Queries
{
    public class GetCoordinatorQuery : IQuery
    {
        public GetCoordinatorQuery(string email)
        {
            this.Email = email;
        }
        public string Email { get; private set; }
    }
}
