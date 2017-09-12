using Common.Layer.CqrsCore;

namespace Business.QueryResults
{
    public class GetPatientEmailQueryResult : IQueryResult
    {
        public GetPatientEmailQueryResult(string email)
        {
            this.Email = email;
        }
        public string Email { get; private set; }
    }
}
