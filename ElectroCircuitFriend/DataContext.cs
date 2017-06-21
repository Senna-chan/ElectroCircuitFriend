using System.Data.Entity;
using ElectroCircuitFriend.Models;

namespace ElectroCircuitFriend
{
    public class DataContext : DbContext
    {
        public DbSet<Battery> Batteries { get; set; }
        public DbSet<Capacitor> Capacitors { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Resistor> Resistors { get; set; }
        public DbSet<Transistor> Transistors { get; set; }
    }
}