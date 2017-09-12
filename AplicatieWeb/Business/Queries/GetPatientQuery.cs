using Common.Layer.CqrsCore;

namespace Business.Queries
{
    public class GetPatientQuery : IQuery
    {
        public GetPatientQuery(string email)
        {
            this.Email = email;
        }
        public string Email { get; private set; }
    }
}
