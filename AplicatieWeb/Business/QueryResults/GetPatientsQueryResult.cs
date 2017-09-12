using Common.Layer.CqrsCore;
using Domain.Model.Models;
using System.Collections.Generic;

namespace Business.QueryResults
{
    public class GetPatientsQueryResult : IQueryResult
    {
        public GetPatientsQueryResult(List<Patient> patients)
        {
            this.Patients = patients;
        }
        public List<Patient> Patients { get; private set; }
    }
}
