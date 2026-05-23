using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealMadridWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddVenueToMatchesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Venue",
                table: "Matches",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Venue",
                table: "Matches");
        }
    }
}
