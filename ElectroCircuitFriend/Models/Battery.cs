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
        public string Voltage { get; set; }
        public string MaxAmpDraw { get; set; }
        public BatteryTypes BatteryType { get; set; }

        public Battery(int capacity, string voltage, string maxAmpDraw, BatteryTypes batteryType)
        {
            Capacity = capacity;
            Voltage = voltage;
            MaxAmpDraw = maxAmpDraw;
            BatteryType = batteryType;
        }
    }
}
