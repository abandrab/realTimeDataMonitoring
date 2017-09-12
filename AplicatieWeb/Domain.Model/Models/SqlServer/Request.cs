using System;

namespace Domain.Model.Models.SqlServer
{
    public class Request : Base
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public string State { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
