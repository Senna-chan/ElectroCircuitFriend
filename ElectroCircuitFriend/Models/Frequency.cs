using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroCircuitFriend.Models
{
    public enum FrequencyLevels
    {
        Hertz,
        KiloHertz,
        MegaHertz,
        GigaHertz
    }
    public class Frequency
    {
        public int Id { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public FrequencyLevels FrequencyLevel { get; set; }

        public Frequency(double min, double max, FrequencyLevels frequencyLevel)
        {
            Min = min;
            Max = max;
            FrequencyLevel = frequencyLevel;
        }
    }
}
