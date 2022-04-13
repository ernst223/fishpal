using Microsoft.EntityFrameworkCore.Migrations;

namespace FishPalAPI.Migrations
{
    public partial class ChangedColumnsFromDescriptionToNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Provinces",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Facets",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Clubs",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Provinces",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Facets",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Clubs",
                newName: "Description");
        }
    }
}
