using System;
using Business.Queries;
using Business.QueryResults;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using System.Linq;
using Domain.Model.Models;
using System.Collections.Generic;

namespace Business.QueryHandlers
{
    public class GetPatientSensorsQueryHandler : IQueryHandler<GetPatientSensorsQuery, GetPatientSensorsQueryResult>
    {
        private readonly IPatientRepository patientRepository;
        public GetPatientSensorsQueryHandler(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }
        public GetPatientSensorsQueryResult Retrieve(GetPatientSensorsQuery query)
        {
            var patient = patientRepository.GetBy(p => p.UserDetails.Email.Equals(query.Email)).FirstOrDefault();
            ICollection<Sensor> sensors = null;
            if(patient != null)
            {
                sensors = patient.Sensors;
            }
            return new GetPatientSensorsQueryResult(sensors);
        }
    }
}
