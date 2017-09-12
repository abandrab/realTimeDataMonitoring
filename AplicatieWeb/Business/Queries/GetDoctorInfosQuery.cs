using Common.Layer.CqrsCore;

namespace Business.Queries
{
    public class GetDoctorInfosQuery : IQuery
    {
        public GetDoctorInfosQuery(string email)
        {
            this.Email = email;
        }
        public string Email { get; private set; }
    }
}
