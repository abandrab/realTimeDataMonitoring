using Common.Layer.CqrsCore;

namespace Business.Queries
{
    public class GetPatientsQuery : IQuery
    {
        public GetPatientsQuery(string email)
        {
            this.Email = email;
        }
        public string Email { get; private set; }
    }
}
