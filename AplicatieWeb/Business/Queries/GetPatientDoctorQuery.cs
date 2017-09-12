using Common.Layer.CqrsCore;

namespace Business.Queries
{
    public class GetPatientDoctorQuery : IQuery
    {
        public GetPatientDoctorQuery(string email)
        {
            Email = email;
        }
        public string Email { get; private set; }
    }
}
