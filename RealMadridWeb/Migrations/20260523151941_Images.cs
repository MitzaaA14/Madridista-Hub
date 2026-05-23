using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealMadridWeb.Migrations
{
    /// <inheritdoc />
    public partial class Images : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Teams_TeamId",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "League",
                table: "Teams");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Teams",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Staff",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Staff",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Sponsors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AwayTeamLogoUrl",
                table: "Matches",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeTeamLogoUrl",
                table: "Matches",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "League",
                table: "Matches",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Teams_TeamId",
                table: "Staff",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Teams_TeamId",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "AwayTeamLogoUrl",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "HomeTeamLogoUrl",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "League",
                table: "Matches");

            migrationBuilder.AddColumn<string>(
                name: "League",
                table: "Teams",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Staff",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Teams_TeamId",
                table: "Staff",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
