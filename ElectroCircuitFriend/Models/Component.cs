using System;
using System.ComponentModel;

namespace ElectroCircuitFriend.Models
{
    public enum ComponentCategories
    {
        Misc,
        Transistor,
        PowerSupply,
        Resistor,
        Diode,
        Led,
        CeramicCap,
        ElectrolyticCap,
        Motor,
        Servo,
        Stepper,
        Driver,
        Microcontroller,
        SingleBoardComputer,
        Cables
    }

    public class Component
    {
        public int Id { get; set; }
        public ComponentCategories ComponentCategory { get; set; }
        public string Name { get; set; }
        public string Disciption { get; set; }
        public string ImageName { get; set; }
        public string DataSheet { get; set; }

        [DefaultValue(0)]
        public int InStock { get; set; }

        [DefaultValue(0)]
        public int Used { get; set; }

        public virtual Amp Amp { get; set; }
        public virtual Battery Battery { get; set; }
        public virtual Capacitor Capacitor { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual Frequency Frequency { get; set; }
        public virtual MicroController MicroController { get; set; }
        public virtual Volt Volt { get; set; }
        public virtual Resistor Resistor { get; set; }
        public virtual Transistor Transistor { get; set; }

        internal Component()
        {
        }

        public Component(ComponentCategories componentCategory, string name, string disciption, string imageName, string dataSheet, int inStock, int used)
        {
            ComponentCategory = componentCategory;
            Name = name;
            Disciption = disciption;
            ImageName = imageName;
            DataSheet = dataSheet;
            InStock = inStock;
            Used = used;
        }

        public void AddVoltage(Volt volt)
        {
            Volt = volt;
        }

        public void AddAmp(Amp amp)
        {
            Amp = amp;
        }

        public void AddTransistor(Transistor transistor)
        {
            Transistor = transistor;
        }

        public void AddCapacitor(Capacitor cap)
        {
            Capacitor = cap;
        }

        public void AddBattery(Battery bat)
        {
            Battery = bat;
        }

        public void AddMicrocontroller(MicroController microController)
        {
            MicroController = microController;
        }

        public void AddFrequency(Frequency frequency)
        {
            Frequency = frequency;
        }

        public void AddDriver(Driver driver)
        {
            Driver = driver;
        }
    }
}