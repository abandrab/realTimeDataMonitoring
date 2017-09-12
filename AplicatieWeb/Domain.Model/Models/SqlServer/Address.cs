using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model.Models
{
    public class Address : Base
    {
        [Key, ForeignKey("UserDetails")]
        public Guid Id { get; set; }

        public UserDetails UserDetails { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
