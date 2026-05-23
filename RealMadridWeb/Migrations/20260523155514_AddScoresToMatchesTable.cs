using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealMadridWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddScoresToMatchesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AwayScore",
                table: "Matches",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HomeScore",
                table: "Matches",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Matches",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwayScore",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "HomeScore",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Matches");
        }
    }
}
