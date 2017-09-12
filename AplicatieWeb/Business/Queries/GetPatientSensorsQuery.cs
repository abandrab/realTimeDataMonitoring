using Common.Layer.CqrsCore;

namespace Business.Queries
{
    public class GetPatientSensorsQuery : IQuery
    {
        public GetPatientSensorsQuery(string email)
        {
            Email = email;
        }
        public string Email { get; private set; }
    }
}
