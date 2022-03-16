using Microsoft.EntityFrameworkCore.Migrations;

namespace FishPalAPI.Migrations
{
    public partial class fixProvinces : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_Facets_FacetId",
                table: "Provinces");

            migrationBuilder.DropIndex(
                name: "IX_Provinces_FacetId",
                table: "Provinces");

            migrationBuilder.DropColumn(
                name: "FacetId",
                table: "Provinces");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Clubs",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FacetProvince",
                columns: table => new
                {
                    FacetsId = table.Column<int>(type: "int", nullable: false),
                    ProvincesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacetProvince", x => new { x.FacetsId, x.ProvincesId });
                    table.ForeignKey(
                        name: "FK_FacetProvince_Facets_FacetsId",
                        column: x => x.FacetsId,
                        principalTable: "Facets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacetProvince_Provinces_ProvincesId",
                        column: x => x.ProvincesId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_UserId",
                table: "Clubs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FacetProvince_ProvincesId",
                table: "FacetProvince",
                column: "ProvincesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_AspNetUsers_UserId",
                table: "Clubs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_AspNetUsers_UserId",
                table: "Clubs");

            migrationBuilder.DropTable(
                name: "FacetProvince");

            migrationBuilder.DropIndex(
                name: "IX_Clubs_UserId",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "FacetId",
                table: "Provinces",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_FacetId",
                table: "Provinces",
                column: "FacetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Provinces_Facets_FacetId",
                table: "Provinces",
                column: "FacetId",
                principalTable: "Facets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
