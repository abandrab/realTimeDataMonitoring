using System;
using Business.Queries;
using Business.QueryResults;
using Common.Layer.CqrsCore;
using Vanguard;
using Sql.Server.Access.Interfaces;
using System.Linq;
using System.Data.Entity;

namespace Business.QueryHandlers
{
    public class GetCoordinatorQueryHandler : IQueryHandler<GetCoordinatorQuery, GetCoordinatorQueryResult>
    {
        private readonly IPatientRepository patientRepository;
        public GetCoordinatorQueryHandler(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }
        public GetCoordinatorQueryResult Retrieve(GetCoordinatorQuery query)
        {
            Guard.ArgumentNotNull(query, nameof(query));
            var coordinatorIp = this.patientRepository.GetBy(p => p.UserDetails.Email == query.Email).Include("Coordinator").FirstOrDefault().Coordinator.IP;
            return new GetCoordinatorQueryResult(coordinatorIp);
        }
    }
}
