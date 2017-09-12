using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Models
{
    public class Coordinator : BaseGuid
    {
        [Required]
        public string  IP { get; set; }
        public int MaxNoPatients { get; set; }
        public virtual Administrator Administrator { get; set; }
        public virtual ICollection<Sensor> Sensors { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
