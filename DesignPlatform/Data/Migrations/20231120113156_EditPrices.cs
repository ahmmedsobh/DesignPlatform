using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Packagees",
                newName: "FrontYardPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "BackYardPrice",
                table: "Packagees",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FrontBackYardPrice",
                table: "Packagees",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackYardPrice",
                table: "Packagees");

            migrationBuilder.DropColumn(
                name: "FrontBackYardPrice",
                table: "Packagees");

            migrationBuilder.RenameColumn(
                name: "FrontYardPrice",
                table: "Packagees",
                newName: "Price");
        }
    }
}
