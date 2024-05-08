using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewVoyager.Migrations
{
    /// <inheritdoc />
    public partial class FinalMigrationHopefulyRIS1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Event",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Event");
        }
    }
}
