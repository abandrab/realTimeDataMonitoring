using System;

namespace Domain.Model.Models.MongoDB
{ 
    public class Entity
    {
        public string PatientId { get; set; }
        public int SensorId { get; set; }
        public string NodeType { get; set; }
        public DateTime? Timestamp { get; set; }
        public int PackNumber { get; set; }

        public void ConvertTimezone()
        {
            if(this.Timestamp !=  null)
            {
                var timestamp = (DateTime)Timestamp;
                TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("E. Europe Standard Time");
                DateTimeOffset offsetConverted = new DateTimeOffset(timestamp, zone.GetUtcOffset(timestamp));
                DateTime roundTripOffset = offsetConverted.DateTime;
                this.Timestamp = roundTripOffset;
            }
        }
    }
}
