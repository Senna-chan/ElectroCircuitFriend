namespace ElectroCircuitFriendRemake.Models
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
    }
}
