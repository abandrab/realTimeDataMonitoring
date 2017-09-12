using System.Collections.Generic;

namespace Domain.Model.Models.MongoDB
{
    public class ECG
    {
        public int EP { get; set; }
        public List<int> ES { get; set; }
        public List<int> ECG1 { get; set; }
        public List<int> ECG2 { get; set; }
        public List<int> ECG3 { get; set; }
    }
}
