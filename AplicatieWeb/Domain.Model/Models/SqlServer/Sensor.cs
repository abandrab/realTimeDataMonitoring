using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Models
{
    public class Sensor : BaseGuid
    {
        [Required]
        public int SensorId { get; set; }
        [Required]
        public int SensorType { get; set; }
    }
}
