using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FishPalAPI.Migrations
{
    public partial class addProvincialInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "provincialInformationId",
                table: "UserInformation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProvincialInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProvinceName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProvincePeriod = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProvinceConstitutionRecieved = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProvinceConstitutionDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ProvinceCodeOfCoductRecieved = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProvinceCodeOfCoductDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ProvinceDressCodeRecieved = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProvinceDressCodeDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ProvinceDisciplinaryCodeRecieved = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProvinceDisciplinaryCodeDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvincialInformation", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProvincialInformationComtteeMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Position = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Period = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProvincialInformationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvincialInformationComtteeMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProvincialInformationComtteeMembers_ProvincialInformation_Pr~",
                        column: x => x.ProvincialInformationId,
                        principalTable: "ProvincialInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProvincialInformationPriorPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Period = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProvincialInformationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvincialInformationPriorPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProvincialInformationPriorPeriods_ProvincialInformation_Prov~",
                        column: x => x.ProvincialInformationId,
                        principalTable: "ProvincialInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserInformation_provincialInformationId",
                table: "UserInformation",
                column: "provincialInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvincialInformationComtteeMembers_ProvincialInformationId",
                table: "ProvincialInformationComtteeMembers",
                column: "ProvincialInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvincialInformationPriorPeriods_ProvincialInformationId",
                table: "ProvincialInformationPriorPeriods",
                column: "ProvincialInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInformation_ProvincialInformation_provincialInformationId",
                table: "UserInformation",
                column: "provincialInformationId",
                principalTable: "ProvincialInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInformation_ProvincialInformation_provincialInformationId",
                table: "UserInformation");

            migrationBuilder.DropTable(
                name: "ProvincialInformationComtteeMembers");

            migrationBuilder.DropTable(
                name: "ProvincialInformationPriorPeriods");

            migrationBuilder.DropTable(
                name: "ProvincialInformation");

            migrationBuilder.DropIndex(
                name: "IX_UserInformation_provincialInformationId",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "provincialInformationId",
                table: "UserInformation");
        }
    }
}
