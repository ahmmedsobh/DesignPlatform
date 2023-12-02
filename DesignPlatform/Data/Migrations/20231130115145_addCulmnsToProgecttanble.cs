using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class addCulmnsToProgecttanble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FYDoYouWantAnyPrivacyNotes",
                table: "Projects",
                newName: "FYDoYouWantAnyPrivacyNotes2");

            migrationBuilder.AddColumn<string>(
                name: "FYDoYouWantAnyPrivacyNotes1",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FYDoYouWantAnyPrivacyNotes1",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "FYDoYouWantAnyPrivacyNotes2",
                table: "Projects",
                newName: "FYDoYouWantAnyPrivacyNotes");
        }
    }
}
