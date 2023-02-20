using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FishPalAPI.Migrations
{
    public partial class addingAcknoledgement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Federation_FederationId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Federation");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FederationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FederationId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "Acknowledged",
                table: "DocumentMessages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Acknowledged",
                table: "DocumentMessages");

            migrationBuilder.AddColumn<int>(
                name: "FederationId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Federation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Federation", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FederationId",
                table: "AspNetUsers",
                column: "FederationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Federation_FederationId",
                table: "AspNetUsers",
                column: "FederationId",
                principalTable: "Federation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
