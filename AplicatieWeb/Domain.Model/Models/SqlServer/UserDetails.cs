using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Models
{
    public class UserDetails : BaseGuid
    {
        [Required]
        public string Role { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool FirstChange { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }

        public virtual Address Address { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Assistant Assistant { get; set; }
        public Administrator Administrator { get; set; }
    }
}