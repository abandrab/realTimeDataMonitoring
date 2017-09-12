using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model.Models
{
    public class Assistant : Base
    {
        [Key, ForeignKey("UserDetails")]
        public Guid Id { get; set; }

        public virtual UserDetails UserDetails { get; set; }
        public virtual IEnumerable<Patient> Patients { get; set; }
    }
}
