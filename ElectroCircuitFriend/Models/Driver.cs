namespace ElectroCircuitFriend.Models
{
    public enum DriverTypes
    {
        Stepper,
        Motor,
        Servo
    }
    public class Driver
    {
        public int Id { get; set; }
        public DriverTypes DriverType { get; set; }
        public virtual Volt MinVolt { get; set; }
        public virtual Volt MaxVolt { get; set; }
        public virtual Amp MaxAmp { get; set; }
        public int Outputs { get; set; }
    }
}