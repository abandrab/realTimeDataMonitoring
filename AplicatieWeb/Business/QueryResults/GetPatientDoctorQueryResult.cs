using Common.Layer.CqrsCore;
using Domain.Model.Models;

namespace Business.QueryResults
{
    public class GetPatientDoctorQueryResult : IQueryResult
    {
        public GetPatientDoctorQueryResult(Doctor doctor)
        {
            Doctor = doctor;
        }
        public Doctor Doctor { get; private set; }
    }
}
