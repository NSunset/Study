using Microsoft.EntityFrameworkCore.Migrations;

namespace Mvc.Migrations
{
    public partial class AddAvatarToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "ApplicationUser",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "ApplicationUser");
        }
    }
}
