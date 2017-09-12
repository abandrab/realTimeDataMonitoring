using Common.Layer.CqrsCore;
using Domain.Model.Models;

namespace Business.QueryResults
{
    public class GetPatientQueryResult : IQueryResult
    {
        public GetPatientQueryResult(Patient patient)
        {
            this.Patient = patient;
        }
        public Patient Patient { get; private set; }
    }
}
