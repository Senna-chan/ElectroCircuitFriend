using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ElectroCircuitFriendRemake.Data;

namespace ElectroCircuitFriendRemake.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ElectroCircuitFriendRemake.Models.Battery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BatteryType");

                    b.Property<double>("Capacity");

                    b.Property<string>("MaxAmpDraw");

                    b.Property<string>("Voltage");

                    b.HasKey("Id");

                    b.ToTable("Batteries");
                });

            modelBuilder.Entity("ElectroCircuitFriendRemake.Models.Capacitor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FaradValue");

                    b.Property<string>("MaxVolt");

                    b.Property<double>("Value");

                    b.HasKey("Id");

                    b.ToTable("Capacitors");
                });

            modelBuilder.Entity("ElectroCircuitFriendRemake.Models.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BatteryId");

                    b.Property<int?>("CapacitorId");

                    b.Property<int>("ComponentCategory");

                    b.Property<string>("ComponentImage");

                    b.Property<string>("ComponentPinoutImage");

                    b.Property<string>("DataSheet");

                    b.Property<string>("Description");

                    b.Property<string>("ExtraDescription");

                    b.Property<int>("InStock");

                    b.Property<string>("Name");

                    b.Property<int?>("ResistorId");

                    b.Property<int?>("TransistorId");

                    b.Property<int>("Used");

                    b.HasKey("Id");

                    b.HasIndex("BatteryId");

                    b.HasIndex("CapacitorId");

                    b.HasIndex("ResistorId");

                    b.HasIndex("TransistorId");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("ElectroCircuitFriendRemake.Models.Resistor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<int>("Band1");

                    b.Property<int>("Band2");

                    b.Property<int>("Band3");

                    b.Property<int?>("Band4");

                    b.Property<int?>("Band5");

                    b.Property<bool>("Use4Bands");

                    b.Property<bool>("Use5Bands");

                    b.HasKey("Id");

                    b.ToTable("Resistors");
                });

            modelBuilder.Entity("ElectroCircuitFriendRemake.Models.Transistor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MaxAmp");

                    b.Property<string>("MaxVolt");

                    b.Property<string>("MinVolt");

                    b.Property<int>("TransistorType");

                    b.HasKey("Id");

                    b.ToTable("Transistors");
                });

            modelBuilder.Entity("ElectroCircuitFriendRemake.Models.Component", b =>
                {
                    b.HasOne("ElectroCircuitFriendRemake.Models.Battery", "Battery")
                        .WithMany()
                        .HasForeignKey("BatteryId");

                    b.HasOne("ElectroCircuitFriendRemake.Models.Capacitor", "Capacitor")
                        .WithMany()
                        .HasForeignKey("CapacitorId");

                    b.HasOne("ElectroCircuitFriendRemake.Models.Resistor", "Resistor")
                        .WithMany()
                        .HasForeignKey("ResistorId");

                    b.HasOne("ElectroCircuitFriendRemake.Models.Transistor", "Transistor")
                        .WithMany()
                        .HasForeignKey("TransistorId");
                });
        }
    }
}
