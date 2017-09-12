using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model.Models
{
    public class BaseGuid : Base
    {
        public BaseGuid()
        {
            Id = Guid.NewGuid();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
    }
}
