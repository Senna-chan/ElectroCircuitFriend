namespace ElectroCircuitFriend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Amps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        IsMilliAmp = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Components",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComponentCategory = c.Int(nullable: false),
                        Name = c.String(),
                        Disciption = c.String(),
                        ImageName = c.String(),
                        DataSheet = c.String(),
                        InStock = c.Int(nullable: false),
                        Used = c.Int(nullable: false),
                        BatteryCapacity = c.Int(),
                        Amp_Id = c.Int(),
                        Battery_Id = c.Int(),
                        Capacitor_Id = c.Int(),
                        Frequency_Id = c.Int(),
                        Resistor_Id = c.Int(),
                        Transistor_Id = c.Int(),
                        Volt_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Amps", t => t.Amp_Id)
                .ForeignKey("dbo.Batteries", t => t.Battery_Id)
                .ForeignKey("dbo.Capacitors", t => t.Capacitor_Id)
                .ForeignKey("dbo.Frequencies", t => t.Frequency_Id)
                .ForeignKey("dbo.Resistors", t => t.Resistor_Id)
                .ForeignKey("dbo.Transistors", t => t.Transistor_Id)
                .ForeignKey("dbo.Volts", t => t.Volt_Id)
                .Index(t => t.Amp_Id)
                .Index(t => t.Battery_Id)
                .Index(t => t.Capacitor_Id)
                .Index(t => t.Frequency_Id)
                .Index(t => t.Resistor_Id)
                .Index(t => t.Transistor_Id)
                .Index(t => t.Volt_Id);
            
            CreateTable(
                "dbo.Batteries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Capacity = c.Double(nullable: false),
                        BatteryType = c.Int(nullable: false),
                        MaxAmpDraw_Id = c.Int(),
                        Voltage_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Amps", t => t.MaxAmpDraw_Id)
                .ForeignKey("dbo.Volts", t => t.Voltage_Id)
                .Index(t => t.MaxAmpDraw_Id)
                .Index(t => t.Voltage_Id);
            
            CreateTable(
                "dbo.Volts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        IsMilliVolt = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Capacitors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        FaradValue = c.Int(nullable: false),
                        MaxVolt_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Volts", t => t.MaxVolt_Id)
                .Index(t => t.MaxVolt_Id);
            
            CreateTable(
                "dbo.Frequencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Min = c.Double(nullable: false),
                        Max = c.Double(nullable: false),
                        FrequencyLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Resistors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Band1 = c.Int(nullable: false),
                        Band2 = c.Int(nullable: false),
                        Band3 = c.Int(nullable: false),
                        Band4 = c.Int(nullable: false),
                        Band5 = c.Int(nullable: false),
                        Use5Bands = c.Boolean(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transistors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransistorType = c.Int(nullable: false),
                        MaxVolt_Id = c.Int(),
                        MinVolt_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Volts", t => t.MaxVolt_Id)
                .ForeignKey("dbo.Volts", t => t.MinVolt_Id)
                .Index(t => t.MaxVolt_Id)
                .Index(t => t.MinVolt_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Components", "Volt_Id", "dbo.Volts");
            DropForeignKey("dbo.Components", "Transistor_Id", "dbo.Transistors");
            DropForeignKey("dbo.Transistors", "MinVolt_Id", "dbo.Volts");
            DropForeignKey("dbo.Transistors", "MaxVolt_Id", "dbo.Volts");
            DropForeignKey("dbo.Components", "Resistor_Id", "dbo.Resistors");
            DropForeignKey("dbo.Components", "Frequency_Id", "dbo.Frequencies");
            DropForeignKey("dbo.Components", "Capacitor_Id", "dbo.Capacitors");
            DropForeignKey("dbo.Capacitors", "MaxVolt_Id", "dbo.Volts");
            DropForeignKey("dbo.Components", "Battery_Id", "dbo.Batteries");
            DropForeignKey("dbo.Batteries", "Voltage_Id", "dbo.Volts");
            DropForeignKey("dbo.Batteries", "MaxAmpDraw_Id", "dbo.Amps");
            DropForeignKey("dbo.Components", "Amp_Id", "dbo.Amps");
            DropIndex("dbo.Transistors", new[] { "MinVolt_Id" });
            DropIndex("dbo.Transistors", new[] { "MaxVolt_Id" });
            DropIndex("dbo.Capacitors", new[] { "MaxVolt_Id" });
            DropIndex("dbo.Batteries", new[] { "Voltage_Id" });
            DropIndex("dbo.Batteries", new[] { "MaxAmpDraw_Id" });
            DropIndex("dbo.Components", new[] { "Volt_Id" });
            DropIndex("dbo.Components", new[] { "Transistor_Id" });
            DropIndex("dbo.Components", new[] { "Resistor_Id" });
            DropIndex("dbo.Components", new[] { "Frequency_Id" });
            DropIndex("dbo.Components", new[] { "Capacitor_Id" });
            DropIndex("dbo.Components", new[] { "Battery_Id" });
            DropIndex("dbo.Components", new[] { "Amp_Id" });
            DropTable("dbo.Transistors");
            DropTable("dbo.Resistors");
            DropTable("dbo.Frequencies");
            DropTable("dbo.Capacitors");
            DropTable("dbo.Volts");
            DropTable("dbo.Batteries");
            DropTable("dbo.Components");
            DropTable("dbo.Amps");
        }
    }
}
