using Common.Layer.CqrsCore;

namespace Business.Queries
{
    public class GetPatientEmailQuery : IQuery
    {
        public GetPatientEmailQuery(string patientId)
        {
            this.PatientId = patientId;
        }
        public string PatientId { get; private set; }
    }
}
