using System.Data.Entity;
using ElectroCircuitFriend.Models;

namespace ElectroCircuitFriend
{
    public class DataContext : DbContext
    {
        public DbSet<Amp> Amps { get; set; }
        public DbSet<Battery> Batteries { get; set; }
        public DbSet<Capacitor> Capacitors { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Frequency> Frequencies { get; set; }
        public DbSet<MicroController> MicroControllers { get; set; }
        public DbSet<Resistor> Resistors { get; set; }
        public DbSet<Transistor> Transistors { get; set; }
        public DbSet<Volt> Volts { get; set; }
    }
}