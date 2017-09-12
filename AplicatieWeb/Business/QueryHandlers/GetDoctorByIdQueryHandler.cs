using Business.Queries;
using Business.QueryResults;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using Vanguard;

namespace Business.QueryHandlers
{
    public class GetDoctorByIdQueryHandler : IQueryHandler<GetDoctorByIdQuery, GetDoctorByIdQueryResult>
    {
        private readonly IDoctorRepository doctorRepository;
        public GetDoctorByIdQueryHandler(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }
        public GetDoctorByIdQueryResult Retrieve(GetDoctorByIdQuery query)
        {
            Guard.ArgumentNotNull(query, nameof(query));

            var doctor = this.doctorRepository.GetById(query.Id);
            return new GetDoctorByIdQueryResult(doctor);
        }
    }
}
