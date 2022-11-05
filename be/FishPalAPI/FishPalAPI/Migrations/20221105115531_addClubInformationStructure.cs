using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FishPalAPI.Migrations
{
    public partial class addClubInformationStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "clubInformationId",
                table: "UserInformation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClubInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClubName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClubPeriod = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClubConstitutionRecieved = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClubConstitutionDateAccepted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ClubCodeOfConductRecieved = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClubCodeOfConductDateAccepted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ClubDisciplinaryCodeRecieved = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClubDisciplinaryCodeDateAccepted = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubInformation", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ClubInformationComitteeMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Position = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Period = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClubInformationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubInformationComitteeMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubInformationComitteeMembers_ClubInformation_ClubInformati~",
                        column: x => x.ClubInformationId,
                        principalTable: "ClubInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ClubInformationPriorPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClubName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClubPeriod = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClubInformationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubInformationPriorPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubInformationPriorPeriods_ClubInformation_ClubInformationId",
                        column: x => x.ClubInformationId,
                        principalTable: "ClubInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserInformation_clubInformationId",
                table: "UserInformation",
                column: "clubInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubInformationComitteeMembers_ClubInformationId",
                table: "ClubInformationComitteeMembers",
                column: "ClubInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubInformationPriorPeriods_ClubInformationId",
                table: "ClubInformationPriorPeriods",
                column: "ClubInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInformation_ClubInformation_clubInformationId",
                table: "UserInformation",
                column: "clubInformationId",
                principalTable: "ClubInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInformation_ClubInformation_clubInformationId",
                table: "UserInformation");

            migrationBuilder.DropTable(
                name: "ClubInformationComitteeMembers");

            migrationBuilder.DropTable(
                name: "ClubInformationPriorPeriods");

            migrationBuilder.DropTable(
                name: "ClubInformation");

            migrationBuilder.DropIndex(
                name: "IX_UserInformation_clubInformationId",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "clubInformationId",
                table: "UserInformation");
        }
    }
}
