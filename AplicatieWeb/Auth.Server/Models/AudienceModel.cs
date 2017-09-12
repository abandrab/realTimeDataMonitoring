using System.ComponentModel.DataAnnotations;

namespace Auth.Server.Models
{
    public class AudienceModel
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
    }
}