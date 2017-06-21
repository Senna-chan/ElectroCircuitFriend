namespace ElectroCircuitFriendRemake.Models
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
        public string MinVolt { get; set; }
        public string MaxVolt { get; set; }
        public string MaxAmp { get; set; }
        public Transistor(TransistorTypes transistorType, string minVolt, string maxVolt, string maxAmp)
        {
            TransistorType = transistorType;
            MinVolt = minVolt;
            MaxVolt = maxVolt;
            MaxAmp = maxAmp;
        }
    }
}
