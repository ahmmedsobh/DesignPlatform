using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class addColumnsToProjectTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoYoWantToHaveAFirePitAreaAnswer",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoYoWantToHaveAFirePitAreaNotes",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoYouHaveOrWantAPergolaOrCoveredPatioAreaAnswer",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoYouHaveOrWantAPergolaOrCoveredPatioAreaNotes",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoYouLikeColorfulOrSimpleGreenAndWhitePlantsAnswer",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoYouLikeColorfulOrSimpleGreenAndWhitePlantsNotes",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoYouWantAGrassAreaAnswer",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoYouWantAGrassAreaNotes",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoYouWantAnyPrivacyAnswer",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoYouWantAnyPrivacyNotes1",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoYouWantAnyPrivacyNotes2",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYDoYouHaveASideYardAnswer",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYDoYouHaveASideYardNotes",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYDoYouLikeColorfulOrSimpleGreenAndWhitePlantsAnswer",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYDoYouLikeColorfulOrSimpleGreenAndWhitePlantsNotes",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYDoYouWantAGrassAreaAnswer",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYDoYouWantAGrassAreaNotes",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYDoYouWantAWaterFeatureAnswer",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYDoYouWantAWaterFeatureNotes",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYDoYouWantAnyPrivacyAnswer",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYDoYouWantAnyPrivacyNotes",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYDoYouWantAnyTallTreesInYourFrontYardAnswer",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYDoYouWantAnyTallTreesInYourFrontYardNotes",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYDoYouWantToDoAnythingDifferentWithYourEntranceAnswer",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYDoYouWantToDoAnythingDifferentWithYourEntranceNotes",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYRegardingTheAmountOfPlantsAreYouLookingForSomethingMoreAnswer",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYRegardingTheAmountOfPlantsAreYouLookingForSomethingMoreNotes",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYWhatAreYouLookingToKeepOrRemoveInYourYardAnswer",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYWhatAreYouLookingToKeepOrRemoveInYourYardNotes",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYWhatIsYourBudgetForYourYardRenovationAnswer",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYWhatIsYourBudgetForYourYardRenovationNotes",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYWhenDoYouPlanOnStartingYourYardRenovationAnswer",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYWhenDoYouPlanOnStartingYourYardRenovationNotes",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYWouldYouLikeToAddAnyTypeOfHardscapeAnswer",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FYWouldYouLikeToAddAnyTypeOfHardscapeNotes",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "DesignDocs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoYoWantToHaveAFirePitAreaAnswer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DoYoWantToHaveAFirePitAreaNotes",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DoYouHaveOrWantAPergolaOrCoveredPatioAreaAnswer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DoYouHaveOrWantAPergolaOrCoveredPatioAreaNotes",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DoYouLikeColorfulOrSimpleGreenAndWhitePlantsAnswer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DoYouLikeColorfulOrSimpleGreenAndWhitePlantsNotes",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DoYouWantAGrassAreaAnswer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DoYouWantAGrassAreaNotes",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DoYouWantAnyPrivacyAnswer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DoYouWantAnyPrivacyNotes1",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DoYouWantAnyPrivacyNotes2",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYDoYouHaveASideYardAnswer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYDoYouHaveASideYardNotes",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYDoYouLikeColorfulOrSimpleGreenAndWhitePlantsAnswer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYDoYouLikeColorfulOrSimpleGreenAndWhitePlantsNotes",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYDoYouWantAGrassAreaAnswer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYDoYouWantAGrassAreaNotes",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYDoYouWantAWaterFeatureAnswer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYDoYouWantAWaterFeatureNotes",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYDoYouWantAnyPrivacyAnswer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYDoYouWantAnyPrivacyNotes",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYDoYouWantAnyTallTreesInYourFrontYardAnswer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYDoYouWantAnyTallTreesInYourFrontYardNotes",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYDoYouWantToDoAnythingDifferentWithYourEntranceAnswer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYDoYouWantToDoAnythingDifferentWithYourEntranceNotes",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYRegardingTheAmountOfPlantsAreYouLookingForSomethingMoreAnswer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYRegardingTheAmountOfPlantsAreYouLookingForSomethingMoreNotes",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYWhatAreYouLookingToKeepOrRemoveInYourYardAnswer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYWhatAreYouLookingToKeepOrRemoveInYourYardNotes",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYWhatIsYourBudgetForYourYardRenovationAnswer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYWhatIsYourBudgetForYourYardRenovationNotes",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYWhenDoYouPlanOnStartingYourYardRenovationAnswer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYWhenDoYouPlanOnStartingYourYardRenovationNotes",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYWouldYouLikeToAddAnyTypeOfHardscapeAnswer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FYWouldYouLikeToAddAnyTypeOfHardscapeNotes",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "DesignDocs");
        }
    }
}
