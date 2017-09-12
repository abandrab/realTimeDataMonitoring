using System;
using Business.Queries;
using Business.QueryResults;
using Common.Layer.CqrsCore;
using Vanguard;
using Sql.Server.Access.Interfaces;
using System.Linq;
using System.Data.Entity;
using Domain.Model.Models;
using System.Collections.Generic;

namespace Business.QueryHandlers
{
    public class GetPatientInfosQueryHandler : IQueryHandler<GetPatientInfosQuery, GetPatientInfosQueryResult>
    {
        private readonly IPatientRepository patientRepository;
        public GetPatientInfosQueryHandler(IPatientRepository patientRepository) 
        {
            this.patientRepository = patientRepository;
        }
        public GetPatientInfosQueryResult Retrieve(GetPatientInfosQuery query)
        {
            Guard.ArgumentNotNull(query, nameof(query));
            var patient = this.patientRepository.GetBy(p => p.UserDetails.Email == query.Email).Include(p => p.Sensors).Include(p => p.Doctor).Include(p => p.Assistant).FirstOrDefault();

            List<int> sensors = new List<int>();
            if(patient.Sensors != null)
            {
                foreach (var sensor in patient.Sensors)
                {
                    sensors.Add(sensor.SensorId);
                }
            }
            var id = patient.Id.ToString();
            string doctorId = null;
            if(patient.Doctor != null)
            {
                doctorId = patient.Doctor.Id.ToString();
            }
            string assistantId = null;
            if (patient.Assistant != null)
            {
                assistantId = patient.Assistant.Id.ToString();
            }

            return new GetPatientInfosQueryResult(new PatientWrapper(id, sensors, doctorId, assistantId));
        }
    }
}
