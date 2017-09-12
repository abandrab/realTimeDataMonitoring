using Business.Queries;
using Business.QueryResults;
using Common.Layer.CqrsCore;
using Vanguard;
using Sql.Server.Access.Interfaces;
using System.Linq;
using Domain.Model.Models.SqlServer;
using System.Collections.Generic;
using Domain.Model.Models;
using Common.Layer.Constants;

namespace Business.QueryHandlers
{
    public class GetRequestsQueryHandler : IQueryHandler<GetRequestsQuery, GetRequestsQueryResult>
    {
        private readonly IRequestRepository requestRepository;
        private readonly IDoctorRepository doctorRepository;

        public GetRequestsQueryHandler(IRequestRepository requestRepository, IDoctorRepository doctorRepository)
        {
            this.requestRepository = requestRepository;
            this.doctorRepository = doctorRepository;
        }
        public GetRequestsQueryResult Retrieve(GetRequestsQuery query)
        {
            Guard.ArgumentNotNull(query, nameof(query));
            var doctor = this.doctorRepository.GetBy(d => d.UserDetails.Email.Equals(query.Email)).FirstOrDefault();
            List<Request> requests = new List<Request>();
            if (doctor != null)
            {
                requests = this.requestRepository.GetBy(r => r.DoctorId.Equals(doctor.Id) && r.State == RequestStates.Pending).ToList();

            }
            return new GetRequestsQueryResult(requests);
        }
    }
}
