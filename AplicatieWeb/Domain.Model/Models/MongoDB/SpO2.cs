using System.Collections.Generic;

namespace Domain.Model.Models.MongoDB
{
    public class SpO2
    {
        public List<int> SW { get; set; }
        public int SO { get; set; }
        public int SI { get; set; }
        public int SP { get; set; }
    }
}
