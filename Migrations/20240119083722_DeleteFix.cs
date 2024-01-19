using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewVoyager.Migrations
{
    /// <inheritdoc />
    public partial class DeleteFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Trip_TripID",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Plan_PlanID",
                table: "Trip");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Trip_TripID",
                table: "Event",
                column: "TripID",
                principalTable: "Trip",
                principalColumn: "TripID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Plan_PlanID",
                table: "Trip",
                column: "PlanID",
                principalTable: "Plan",
                principalColumn: "PlanID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Trip_TripID",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Plan_PlanID",
                table: "Trip");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Trip_TripID",
                table: "Event",
                column: "TripID",
                principalTable: "Trip",
                principalColumn: "TripID");

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Plan_PlanID",
                table: "Trip",
                column: "PlanID",
                principalTable: "Plan",
                principalColumn: "PlanID");
        }
    }
}
