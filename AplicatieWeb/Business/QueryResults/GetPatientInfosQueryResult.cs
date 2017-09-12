using Common.Layer.CqrsCore;
using Domain.Model.Models;
using System.Collections.Generic;

namespace Business.QueryResults
{
    public class GetPatientInfosQueryResult : IQueryResult
    {
        public GetPatientInfosQueryResult(PatientWrapper patient)
        {
            this.Patient = patient;
        }
        public PatientWrapper Patient { get; private set; }
    }
}
