namespace ElectroCircuitFriend.Models
{
    public class MicroController
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Volt LogicLevelVolt { get; set; }
        public virtual Volt SupplyVolt { get; set; }
        public virtual Amp LogicLevelAmp { get; set; }
        public virtual Frequency ClockFrequency { get; set; }

    }
}