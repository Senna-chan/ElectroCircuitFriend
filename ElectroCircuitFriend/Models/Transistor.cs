using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroCircuitFriend.Models
{
    public enum TransistorTypes
    {
        NPN,
        PNP,
        PChannel,
        NChannel
    }

    public class Transistor
    {
        public int Id { get; set; }
        public TransistorTypes TransistorType{ get; set; }
        public Volt MinVolt { get; set; }
        public Volt MaxVolt { get; set; }

        public Transistor(TransistorTypes transistorType, Volt minVolt, Volt maxVolt)
        {
            TransistorType = transistorType;
            MinVolt = minVolt;
            MaxVolt = maxVolt;
        }
    }
}
