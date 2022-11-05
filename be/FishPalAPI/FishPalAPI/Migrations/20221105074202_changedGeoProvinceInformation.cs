using Microsoft.EntityFrameworkCore.Migrations;

namespace FishPalAPI.Migrations
{
    public partial class changedGeoProvinceInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInformation_GeoProvinecInformation_geoProvineInformation~",
                table: "UserInformation");

            migrationBuilder.RenameColumn(
                name: "geoProvineInformationId",
                table: "UserInformation",
                newName: "geoProvinceInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_UserInformation_geoProvineInformationId",
                table: "UserInformation",
                newName: "IX_UserInformation_geoProvinceInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInformation_GeoProvinecInformation_geoProvinceInformatio~",
                table: "UserInformation",
                column: "geoProvinceInformationId",
                principalTable: "GeoProvinecInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInformation_GeoProvinecInformation_geoProvinceInformatio~",
                table: "UserInformation");

            migrationBuilder.RenameColumn(
                name: "geoProvinceInformationId",
                table: "UserInformation",
                newName: "geoProvineInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_UserInformation_geoProvinceInformationId",
                table: "UserInformation",
                newName: "IX_UserInformation_geoProvineInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInformation_GeoProvinecInformation_geoProvineInformation~",
                table: "UserInformation",
                column: "geoProvineInformationId",
                principalTable: "GeoProvinecInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
