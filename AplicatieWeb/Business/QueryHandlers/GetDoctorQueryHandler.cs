using Business.Queries;
using Business.QueryResults;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using System.Linq;
using Vanguard;

namespace Business.QueryHandlers
{
    public class GetDoctorQueryHandler : IQueryHandler<GetDoctorQuery, GetDoctorQueryResult>
    {
        private readonly IDoctorRepository doctorRepository;
        public GetDoctorQueryHandler(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }
        public GetDoctorQueryResult Retrieve(GetDoctorQuery query)
        {
            Guard.ArgumentNotNull(query, nameof(query));

            var doctor = this.doctorRepository.GetBy(p => p.UserDetails.Email == query.Email).FirstOrDefault();
            doctor.UserDetails.Password = null;
            return new GetDoctorQueryResult(doctor);
        }
    }
}
