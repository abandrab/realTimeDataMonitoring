using Common.Layer.CqrsCore;

namespace Business.Queries
{
    public class GetPatientRequestQuery : IQuery
    {
        public GetPatientRequestQuery(string email)
        {
            Email = email;
        }
        public string Email { get; private set; }
    }
}
