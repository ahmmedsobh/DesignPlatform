using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class MainRolesToAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainRoles",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainRoles",
                table: "AspNetUsers");
        }
    }
}
