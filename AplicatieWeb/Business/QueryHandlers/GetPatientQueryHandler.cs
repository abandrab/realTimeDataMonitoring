using Business.Queries;
using Business.QueryResults;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using System.Linq;
using Vanguard;

namespace Business.QueryHandlers
{
    public class GetPatientQueryHandler : IQueryHandler<GetPatientQuery, GetPatientQueryResult>
    {
        private readonly IPatientRepository patientRepository;
        public GetPatientQueryHandler(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }
        public GetPatientQueryResult Retrieve(GetPatientQuery query)
        {
            Guard.ArgumentNotNull(query, nameof(query));

            var patient = this.patientRepository.GetBy(p => p.UserDetails.Email == query.Email).FirstOrDefault();
            patient.UserDetails.Password = null;
            return new GetPatientQueryResult(patient);
        }
    }
}
