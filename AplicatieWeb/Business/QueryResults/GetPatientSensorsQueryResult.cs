using Common.Layer.CqrsCore;
using Domain.Model.Models;
using System.Collections.Generic;

namespace Business.QueryResults
{
    public class GetPatientSensorsQueryResult : IQueryResult
    {
        public GetPatientSensorsQueryResult(ICollection<Sensor> sensors)
        {
            Sensors = sensors;
        }
        public ICollection<Sensor> Sensors { get; private set; }
    }
}
