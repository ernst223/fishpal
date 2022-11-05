using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FishPalAPI.Migrations
{
    public partial class addedGeoProvinceInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "geoProvineInformationId",
                table: "UserInformation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GeoProvinecInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GeoProvince = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProvincialSasaccManagement = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Position = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoProvinecInformation", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserInformation_geoProvineInformationId",
                table: "UserInformation",
                column: "geoProvineInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInformation_GeoProvinecInformation_geoProvineInformation~",
                table: "UserInformation",
                column: "geoProvineInformationId",
                principalTable: "GeoProvinecInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInformation_GeoProvinecInformation_geoProvineInformation~",
                table: "UserInformation");

            migrationBuilder.DropTable(
                name: "GeoProvinecInformation");

            migrationBuilder.DropIndex(
                name: "IX_UserInformation_geoProvineInformationId",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "geoProvineInformationId",
                table: "UserInformation");
        }
    }
}
