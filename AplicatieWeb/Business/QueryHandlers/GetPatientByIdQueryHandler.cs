using System;
using Business.Queries;
using Business.QueryResults;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using Vanguard;

namespace Business.QueryHandlers
{
    public class GetPatientByIdQueryHandler : IQueryHandler<GetPatientByIdQuery, GetPatientByIdQueryResult>
    {
        private readonly IPatientRepository patientRepository;

        public GetPatientByIdQueryHandler(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }
        public GetPatientByIdQueryResult Retrieve(GetPatientByIdQuery query)
        {
            Guard.ArgumentNotNull(query, nameof(query));

            var patient = patientRepository.GetById(query.PatientId);
            return new GetPatientByIdQueryResult(patient);
        }
    }
}
