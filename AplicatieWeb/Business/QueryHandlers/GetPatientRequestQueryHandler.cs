using System;
using Business.Queries;
using Business.QueryResults;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using Vanguard;
using System.Linq;
using Domain.Model.Models.SqlServer;

namespace Business.QueryHandlers
{
    public class GetPatientRequestQueryHandler : IQueryHandler<GetPatientRequestQuery, GetPatientRequestQueryResult>
    {
        private readonly IRequestRepository requestRepository;
        private readonly IPatientRepository patientRepository;

        public GetPatientRequestQueryHandler(IRequestRepository requestRepository,
            IPatientRepository patientRepository)
        {
            this.requestRepository = requestRepository;
            this.patientRepository = patientRepository;
        }
        public GetPatientRequestQueryResult Retrieve(GetPatientRequestQuery query)
        {
            Guard.ArgumentNotNull(query, nameof(query));

            var patient = patientRepository.GetBy(p => p.UserDetails.Email.Equals(query.Email)).FirstOrDefault();
            Request request = null;
            if (patient != null)
            {
                request = requestRepository.GetBy(r => r.PatientId.Equals(patient.Id)).FirstOrDefault();
            }
            return new GetPatientRequestQueryResult(request);
        }
    }
}
