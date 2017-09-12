using System;
using Business.Queries;
using Business.QueryResults;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using Vanguard;
using System.Linq;

namespace Business.QueryHandlers
{
    public class GetDoctorInfosQueryHandler : IQueryHandler<GetDoctorInfosQuery, GetDoctorInfosQueryResult>
    {
        private readonly IDoctorRepository doctorRespository;
        public GetDoctorInfosQueryHandler(IDoctorRepository doctorRespository)
        {
            this.doctorRespository = doctorRespository;
        }
        public GetDoctorInfosQueryResult Retrieve(GetDoctorInfosQuery query)
        {
            Guard.ArgumentNotNull(query, nameof(query));
            var doctor = this.doctorRespository.GetBy(d => d.UserDetails.Email == query.Email).FirstOrDefault();
            string id = null;
            if (doctor != null)
            {
                id = doctor.Id.ToString();
            }
            return new GetDoctorInfosQueryResult(id);
        }
    }
}
