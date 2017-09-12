using System.Collections.Generic;

namespace Domain.Model.Models
{
    public class PatientWrapper
    {
        public PatientWrapper(string id, List<int> sensors, 
            string doctorId, string assistantId)
        {
            this.Id = id;
            this.SensorIds = sensors;
            this.DoctorId = doctorId;
            this.AssistantId = assistantId;
        }
        public string Id { get; set; }
        public List<int> SensorIds { get; set; }
        public string DoctorId { get; private set; }
        public string AssistantId { get; private set; }
    }
}
