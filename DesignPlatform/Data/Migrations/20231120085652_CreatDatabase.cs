using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Packagees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packagees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DesignImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectManagerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DesignerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    YouWantADesignForWahtAreaAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YouWantADesignForWahtAreaNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhatSortOfStyleAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhatSortOfStyleNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoYouHaveKidsAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoYouHaveKidsNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoyouEntertainALotAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoyouEntertainALotNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsThePropertyVisibleOnGoogleMapsAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsThePropertyVisibleOnGoogleMapsNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhatYourAreLookingToKeepAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhatYourAreLookingToKeepNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WouldYouLikeToAddHardscapeAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WouldYouLikeToAddHardscapeNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegardingTheAmountOfPlantsAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegardingTheAmountOfPlantsNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoYouWantAWaterFeatureAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoYouWantAWaterFeatureNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoYouWantABBQAreaAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoYouWantABBQAreaNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SummaryNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DesignerSkillsSet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScopeAndSizeOfProject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointmentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DesignStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DesignerNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Projects_AspNetUsers_DesignerId",
                        column: x => x.DesignerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Projects_AspNetUsers_ProjectManagerId",
                        column: x => x.ProjectManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SubPackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubPackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackageFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageFeatures_Packagees_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packagees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesignDocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignDocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignDocs_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPackages_Packagees_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packagees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectPackages_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageSubPackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    SubPackageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageSubPackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageSubPackages_Packagees_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packagees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageSubPackages_SubPackages_SubPackageId",
                        column: x => x.SubPackageId,
                        principalTable: "SubPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectSubPackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    SubPackageId = table.Column<int>(type: "int", nullable: false),
                    ProjectSubPackageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSubPackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectSubPackages_ProjectSubPackages_ProjectSubPackageId",
                        column: x => x.ProjectSubPackageId,
                        principalTable: "ProjectSubPackages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectSubPackages_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectSubPackages_SubPackages_SubPackageId",
                        column: x => x.SubPackageId,
                        principalTable: "SubPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DesignDocs_ProjectId",
                table: "DesignDocs",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProjectId",
                table: "Images",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageFeatures_PackageId",
                table: "PackageFeatures",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageSubPackages_PackageId",
                table: "PackageSubPackages",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageSubPackages_SubPackageId",
                table: "PackageSubPackages",
                column: "SubPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPackages_PackageId",
                table: "ProjectPackages",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPackages_ProjectId",
                table: "ProjectPackages",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ClientId",
                table: "Projects",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DesignerId",
                table: "Projects",
                column: "DesignerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectManagerId",
                table: "Projects",
                column: "ProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSubPackages_ProjectId",
                table: "ProjectSubPackages",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSubPackages_ProjectSubPackageId",
                table: "ProjectSubPackages",
                column: "ProjectSubPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSubPackages_SubPackageId",
                table: "ProjectSubPackages",
                column: "SubPackageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesignDocs");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "PackageFeatures");

            migrationBuilder.DropTable(
                name: "PackageSubPackages");

            migrationBuilder.DropTable(
                name: "ProjectPackages");

            migrationBuilder.DropTable(
                name: "ProjectSubPackages");

            migrationBuilder.DropTable(
                name: "Packagees");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "SubPackages");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "AspNetUsers");
        }
    }
}
