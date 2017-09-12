using Common.Layer.CqrsCore;
using Domain.Model.Models;

namespace Business.QueryResults
{
    public class GetDoctorQueryResult : IQueryResult
    {
        public GetDoctorQueryResult(Doctor doctor)
        {
            this.Doctor = doctor;
        }
        public Doctor Doctor { get; private set; }
    }
}
