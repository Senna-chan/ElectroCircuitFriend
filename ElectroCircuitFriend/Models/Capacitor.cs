namespace ElectroCircuitFriend.Models
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
        public Volt MaxVolt { get; set; }

        public Capacitor(double value, FaradValueNames faradValue, Volt maxVolt)
        {
            Value = value;
            FaradValue = faradValue;
            MaxVolt = maxVolt;
        }
    }
}