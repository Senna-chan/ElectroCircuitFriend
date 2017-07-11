using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ElectroCircuitFriendRemake.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Batteries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BatteryType = table.Column<int>(nullable: false),
                    Capacity = table.Column<double>(nullable: false),
                    MaxAmpDraw = table.Column<string>(nullable: true),
                    Voltage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batteries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Capacitors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FaradValue = table.Column<int>(nullable: false),
                    MaxVolt = table.Column<string>(nullable: true),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capacitors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resistors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(nullable: false),
                    Band1 = table.Column<int>(nullable: false),
                    Band2 = table.Column<int>(nullable: false),
                    Band3 = table.Column<int>(nullable: false),
                    Band4 = table.Column<int>(nullable: true),
                    Band5 = table.Column<int>(nullable: true),
                    Use4Bands = table.Column<bool>(nullable: false),
                    Use5Bands = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resistors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transistors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaxAmp = table.Column<string>(nullable: true),
                    MaxVolt = table.Column<string>(nullable: true),
                    MinVolt = table.Column<string>(nullable: true),
                    TransistorType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transistors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BatteryId = table.Column<int>(nullable: true),
                    CapacitorId = table.Column<int>(nullable: true),
                    ComponentCategory = table.Column<int>(nullable: false),
                    ComponentImage = table.Column<bool>(nullable: false),
                    ComponentPinoutImage = table.Column<bool>(nullable: false),
                    DataSheet = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ExtraDescription = table.Column<string>(nullable: true),
                    InStock = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedString = table.Column<string>(nullable: true),
                    ResistorId = table.Column<int>(nullable: true),
                    TransistorId = table.Column<int>(nullable: true),
                    Used = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Components_Batteries_BatteryId",
                        column: x => x.BatteryId,
                        principalTable: "Batteries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Components_Capacitors_CapacitorId",
                        column: x => x.CapacitorId,
                        principalTable: "Capacitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Components_Resistors_ResistorId",
                        column: x => x.ResistorId,
                        principalTable: "Resistors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Components_Transistors_TransistorId",
                        column: x => x.TransistorId,
                        principalTable: "Transistors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Components_BatteryId",
                table: "Components",
                column: "BatteryId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_CapacitorId",
                table: "Components",
                column: "CapacitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_ResistorId",
                table: "Components",
                column: "ResistorId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_TransistorId",
                table: "Components",
                column: "TransistorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "Batteries");

            migrationBuilder.DropTable(
                name: "Capacitors");

            migrationBuilder.DropTable(
                name: "Resistors");

            migrationBuilder.DropTable(
                name: "Transistors");
        }
    }
}
