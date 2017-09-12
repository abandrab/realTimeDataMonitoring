using Business.Queries;
using Business.QueryResults;
using Common.Layer.CqrsCore;
using Domain.Model.Models;
using Sql.Server.Access.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Vanguard;

namespace Business.QueryHandlers
{
    public class GetPatientsQueryHandler : IQueryHandler<GetPatientsQuery, GetPatientsQueryResult>
    {
        private readonly IDoctorRepository doctorRepository;
        public GetPatientsQueryHandler(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }
        public GetPatientsQueryResult Retrieve(GetPatientsQuery query)
        {
            Guard.ArgumentNotNull(query, nameof(query));
            var doctor = doctorRepository.GetBy(d => d.UserDetails.Email.Equals(query.Email)).FirstOrDefault();
            List<Patient> patients = new List<Patient>();
            if (doctor != null && doctor.Patients != null)
            {
                patients = doctor.Patients.ToList();
            }
            return new GetPatientsQueryResult(patients);   
        }
    }
}
