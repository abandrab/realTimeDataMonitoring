using System;
using Business.Queries;
using Business.QueryResults;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using System.Linq;
using Domain.Model.Models;
using Vanguard;

namespace Business.QueryHandlers
{
    public class GetPatientDoctorQueryHandler : IQueryHandler<GetPatientDoctorQuery, GetPatientDoctorQueryResult>
    {
        private readonly IPatientRepository patientRepository;
        public GetPatientDoctorQueryHandler(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }
        public GetPatientDoctorQueryResult Retrieve(GetPatientDoctorQuery query)
        {
            Guard.ArgumentNotNull(query, nameof(query));

            var patient = patientRepository.GetBy(p => p.UserDetails.Email.Equals(query.Email)).FirstOrDefault();
            Doctor doctor = null;
            if (patient != null)
            {
                doctor = patient.Doctor;
            }
            return new GetPatientDoctorQueryResult(doctor);
        }
    }
}
