using Domain.Model.Models.SqlServer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model.Models
{
    public class Patient : Base
    {
        [Key,ForeignKey("UserDetails")]
        public Guid Id { get; set; }

        public string CNP { get; set; }
        public string RangeNumber { get; set; }
        public DateTime? Birthdate { get; set; }
        public Guid? DoctorId { get; set; }
        public virtual UserDetails UserDetails { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Assistant Assistant { get; set; }
        public virtual ICollection<Sensor> Sensors { get; set; }
        public virtual Coordinator Coordinator { get; set; }
        public virtual Request Request { get; set; }
    }
}
