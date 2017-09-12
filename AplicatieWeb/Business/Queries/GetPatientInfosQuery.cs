using Common.Layer.CqrsCore;

namespace Business.Queries
{
    public class GetPatientInfosQuery : IQuery
    {
        public GetPatientInfosQuery(string email)
        {
            this.Email = email;
        }
        public string Email { get; private set; }
    }
}
