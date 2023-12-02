using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnAppointMentPhone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppointmentPhone",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentPhone",
                table: "Projects");
        }
    }
}
