using Common.Layer.CqrsCore;
using Domain.Model.Models;

namespace Business.QueryResults
{
    public class GetDoctorByIdQueryResult : IQueryResult
    {
        public GetDoctorByIdQueryResult(Doctor doctor)
        {
            Doctor = doctor;
        }
        public Doctor Doctor { get; private set; }
    }
}
