using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroCircuitFriend.Models
{
    public enum BatteryTypes
    {
        NiCD,
        NiMH,
        LiPo,
        LiIon,
        LiFePo,
        LeadAcid,
        Alkaline
    }

    public class Battery
    {
        public int Id { get; set; }
        public double Capacity { get; set; }
        public virtual Volt Voltage { get; set; }
        public virtual Amp MaxAmpDraw { get; set; }
        public BatteryTypes BatteryType { get; set; }

        public Battery(int capacity, Volt voltage, Amp maxAmpDraw, BatteryTypes batteryType)
        {
            Capacity = capacity;
            Voltage = voltage;
            MaxAmpDraw = maxAmpDraw;
            BatteryType = batteryType;
        }
    }
}
