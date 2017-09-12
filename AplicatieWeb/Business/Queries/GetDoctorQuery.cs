using Common.Layer.CqrsCore;

namespace Business.Queries
{
    public class GetDoctorQuery : IQuery
    {
        public GetDoctorQuery(string email)
        {
            this.Email = email;
        }
        public string Email { get; private set; }
    }
}
