using Microsoft.EntityFrameworkCore.Migrations;

namespace FishPalAPI.Migrations
{
    public partial class addFederationToFacet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Federation_federationId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_federationId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "federationId",
                table: "UserProfiles");

            migrationBuilder.AddColumn<string>(
                name: "Base64String",
                table: "Facets",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64String",
                table: "Facets");

            migrationBuilder.AddColumn<int>(
                name: "federationId",
                table: "UserProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_federationId",
                table: "UserProfiles",
                column: "federationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Federation_federationId",
                table: "UserProfiles",
                column: "federationId",
                principalTable: "Federation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
