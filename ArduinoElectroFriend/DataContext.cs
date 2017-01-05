using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArduinoElectroFriend.Models;

namespace ArduinoElectroFriend
{
    public class DataContext : DbContext
    {
        public DbSet<Resistor> Resistors { get; set; }
        public DbSet<Component> Components { get; set; }
    }
}
