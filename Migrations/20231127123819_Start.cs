using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewVoyager.Migrations
{
    /// <inheritdoc />
    public partial class Start : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Voyager",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voyager", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Plan",
                columns: table => new
                {
                    PlanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Attendees = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoyagerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.PlanID);
                    table.ForeignKey(
                        name: "FK_Plan_Voyager_VoyagerID",
                        column: x => x.VoyagerID,
                        principalTable: "Voyager",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Trip",
                columns: table => new
                {
                    TripID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => x.TripID);
                    table.ForeignKey(
                        name: "FK_Trip_Plan_PlanID",
                        column: x => x.PlanID,
                        principalTable: "Plan",
                        principalColumn: "PlanID");
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderNum = table.Column<int>(type: "int", nullable: true),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TripsTripID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_Event_Trip_TripsTripID",
                        column: x => x.TripsTripID,
                        principalTable: "Trip",
                        principalColumn: "TripID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_TripsTripID",
                table: "Event",
                column: "TripsTripID");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_VoyagerID",
                table: "Plan",
                column: "VoyagerID");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_PlanID",
                table: "Trip",
                column: "PlanID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Trip");

            migrationBuilder.DropTable(
                name: "Plan");

            migrationBuilder.DropTable(
                name: "Voyager");
        }
    }
}
