namespace ElectroCircuitFriendRemake.Models
{
    public enum FaradValueNames
    {
        pF,
        nF,
        µF,
        mF,
        F,
        kF
    }
    public class Capacitor
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public FaradValueNames FaradValue { get; set; }
        public string MaxVolt { get; set; }
    }
}