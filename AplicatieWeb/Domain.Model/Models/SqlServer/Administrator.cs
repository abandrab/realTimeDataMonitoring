using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model.Models
{
    public class Administrator : Base
    {
        [Key, ForeignKey("UserDetails")]
        public Guid Id { get; set; }

        [Required]
        public virtual UserDetails UserDetails { get; set; }
        public virtual IEnumerable<Coordinator> Coordinators { get; set; }
    }
}
