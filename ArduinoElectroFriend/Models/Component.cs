namespace ArduinoElectroFriend.Models
{
    public enum ComponentCategories
    {
        Transistor,
        Mosfet,
        PowerSupply,
        Resistor,
        Motor,
        Servo,
        Stepper,
        Driver,
        Microcontroller,
        SingleBoardComputer,
        Misc
    }
    public class Component
    {
        public int Id { get; set; }
        public ComponentCategories ComponentCategory { get; set; }
        public int Amount { get; set; }
    }
}