using Business.Queries;
using Business.QueryResults;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using System;
using Vanguard;

namespace Business.QueryHandlers
{
    public class GetPatientEmailQueryHandler : IQueryHandler<GetPatientEmailQuery, GetPatientEmailQueryResult>
    {
        private readonly IPatientRepository patientRepository;
        public GetPatientEmailQueryHandler(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }
        public GetPatientEmailQueryResult Retrieve(GetPatientEmailQuery query)
        {
            Guard.ArgumentNotNull(query, nameof(query));

            var patient = this.patientRepository.GetById(new Guid(query.PatientId));
            string email = null;
            if(patient != null)
            {
                email = patient.UserDetails.Email;
            }
            return new GetPatientEmailQueryResult(email);
        }
    }
}
