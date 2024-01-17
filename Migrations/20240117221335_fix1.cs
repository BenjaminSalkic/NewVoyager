using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewVoyager.Migrations
{
    /// <inheritdoc />
    public partial class fix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Trip_TripsTripID",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "TripsTripID",
                table: "Event",
                newName: "TripID");

            migrationBuilder.RenameIndex(
                name: "IX_Event_TripsTripID",
                table: "Event",
                newName: "IX_Event_TripID");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Trip_TripID",
                table: "Event",
                column: "TripID",
                principalTable: "Trip",
                principalColumn: "TripID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Trip_TripID",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "TripID",
                table: "Event",
                newName: "TripsTripID");

            migrationBuilder.RenameIndex(
                name: "IX_Event_TripID",
                table: "Event",
                newName: "IX_Event_TripsTripID");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Trip_TripsTripID",
                table: "Event",
                column: "TripsTripID",
                principalTable: "Trip",
                principalColumn: "TripID");
        }
    }
}
