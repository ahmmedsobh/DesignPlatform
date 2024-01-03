using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConfirmPaymentColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ConfirmPayment",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmPayment",
                table: "Projects");
        }
    }
}
