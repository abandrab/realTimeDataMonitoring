using Domain.Model.Models.SqlServer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model.Models
{
    public class Doctor : Base
    {
        [Key, ForeignKey("UserDetails")]
        public Guid Id { get; set; }

        public virtual UserDetails UserDetails { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
