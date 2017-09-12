using Common.Layer.CqrsCore;
using Domain.Model.Models;

namespace Business.QueryResults
{
    public class GetPatientByIdQueryResult : IQueryResult
    {
        public GetPatientByIdQueryResult(Patient patient)
        {
            Patient = patient;
        }
        public Patient Patient { get; set; }
    }
}
