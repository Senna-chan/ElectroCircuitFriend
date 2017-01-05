using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroCircuitFriend.Models
{
    public class Volt
    {
        public int Id { get; set; }
        public double Value { get; set; }
        [DefaultValue(false)]
        public bool IsMilliVolt { get; set; }

        public Volt(double maxVolt, bool isMilliVolt)
        {
            Value = maxVolt;
            IsMilliVolt = isMilliVolt;
        }

        internal Volt()
        {
            
        }
    }
}
