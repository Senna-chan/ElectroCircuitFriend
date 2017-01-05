namespace ElectroCircuitFriend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedThings : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Components", "Frequency_Id", "dbo.Frequencies");
            DropIndex("dbo.Components", new[] { "Frequency_Id" });
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DriverType = c.Int(nullable: false),
                        Outputs = c.Int(nullable: false),
                        MaxAmp_Id = c.Int(),
                        MaxVolt_Id = c.Int(),
                        MinVolt_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Amps", t => t.MaxAmp_Id)
                .ForeignKey("dbo.Volts", t => t.MaxVolt_Id)
                .ForeignKey("dbo.Volts", t => t.MinVolt_Id)
                .Index(t => t.MaxAmp_Id)
                .Index(t => t.MaxVolt_Id)
                .Index(t => t.MinVolt_Id);
            
            CreateTable(
                "dbo.MicroControllers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ClockFrequency_Id = c.Int(),
                        LogicLevelAmp_Id = c.Int(),
                        LogicLevelVolt_Id = c.Int(),
                        SupplyVolt_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Frequencies", t => t.ClockFrequency_Id)
                .ForeignKey("dbo.Amps", t => t.LogicLevelAmp_Id)
                .ForeignKey("dbo.Volts", t => t.LogicLevelVolt_Id)
                .ForeignKey("dbo.Volts", t => t.SupplyVolt_Id)
                .Index(t => t.ClockFrequency_Id)
                .Index(t => t.LogicLevelAmp_Id)
                .Index(t => t.LogicLevelVolt_Id)
                .Index(t => t.SupplyVolt_Id);
            
            AddColumn("dbo.Components", "Driver_Id", c => c.Int());
            CreateIndex("dbo.Components", "Driver_Id");
            AddForeignKey("dbo.Components", "Driver_Id", "dbo.Drivers", "Id");
            DropColumn("dbo.Components", "BatteryCapacity");
            DropColumn("dbo.Components", "Frequency_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Components", "Frequency_Id", c => c.Int());
            AddColumn("dbo.Components", "BatteryCapacity", c => c.Int());
            DropForeignKey("dbo.MicroControllers", "SupplyVolt_Id", "dbo.Volts");
            DropForeignKey("dbo.MicroControllers", "LogicLevelVolt_Id", "dbo.Volts");
            DropForeignKey("dbo.MicroControllers", "LogicLevelAmp_Id", "dbo.Amps");
            DropForeignKey("dbo.MicroControllers", "ClockFrequency_Id", "dbo.Frequencies");
            DropForeignKey("dbo.Components", "Driver_Id", "dbo.Drivers");
            DropForeignKey("dbo.Drivers", "MinVolt_Id", "dbo.Volts");
            DropForeignKey("dbo.Drivers", "MaxVolt_Id", "dbo.Volts");
            DropForeignKey("dbo.Drivers", "MaxAmp_Id", "dbo.Amps");
            DropIndex("dbo.MicroControllers", new[] { "SupplyVolt_Id" });
            DropIndex("dbo.MicroControllers", new[] { "LogicLevelVolt_Id" });
            DropIndex("dbo.MicroControllers", new[] { "LogicLevelAmp_Id" });
            DropIndex("dbo.MicroControllers", new[] { "ClockFrequency_Id" });
            DropIndex("dbo.Drivers", new[] { "MinVolt_Id" });
            DropIndex("dbo.Drivers", new[] { "MaxVolt_Id" });
            DropIndex("dbo.Drivers", new[] { "MaxAmp_Id" });
            DropIndex("dbo.Components", new[] { "Driver_Id" });
            DropColumn("dbo.Components", "Driver_Id");
            DropTable("dbo.MicroControllers");
            DropTable("dbo.Drivers");
            CreateIndex("dbo.Components", "Frequency_Id");
            AddForeignKey("dbo.Components", "Frequency_Id", "dbo.Frequencies", "Id");
        }
    }
}
