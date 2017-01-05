using System.ComponentModel;

namespace ElectroCircuitFriend.Models
{
    public class Amp
    {
        public int Id { get; set; }
        public double Value { get; set; }
        [DefaultValue(false)]
        public bool IsMilliAmp { get; set; }

        public Amp(double value, bool isMilliAmp)
        {
            Value = value;
            IsMilliAmp = isMilliAmp;
        }
        internal Amp() { }
    }
}