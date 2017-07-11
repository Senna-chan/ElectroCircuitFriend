using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectroCircuitFriendRemake.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectroCircuitFriendRemake.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=ElectroCircuitFriend;Trusted_Connection=True;MultipleActiveResultSets=true");
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Battery> Batteries { get; set; }
        public DbSet<Capacitor> Capacitors { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Resistor> Resistors { get; set; }
        public DbSet<Transistor> Transistors { get; set; }
    }
}
