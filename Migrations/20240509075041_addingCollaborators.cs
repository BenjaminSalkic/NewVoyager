using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewVoyager.Migrations
{
    /// <inheritdoc />
    public partial class addingCollaborators : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUserPlans",
                columns: table => new
                {
                    CollaborationsPlanID = table.Column<int>(type: "int", nullable: false),
                    CollaboratorsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserPlans", x => new { x.CollaborationsPlanID, x.CollaboratorsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserPlans_AspNetUsers_CollaboratorsId",
                        column: x => x.CollaboratorsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserPlans_Plan_CollaborationsPlanID",
                        column: x => x.CollaborationsPlanID,
                        principalTable: "Plan",
                        principalColumn: "PlanID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserPlans_CollaboratorsId",
                table: "ApplicationUserPlans",
                column: "CollaboratorsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserPlans");
        }
    }
}
