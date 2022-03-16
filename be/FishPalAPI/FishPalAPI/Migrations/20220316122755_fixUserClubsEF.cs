using Microsoft.EntityFrameworkCore.Migrations;

namespace FishPalAPI.Migrations
{
    public partial class fixUserClubsEF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_AspNetUsers_UserId",
                table: "Clubs");

            migrationBuilder.DropIndex(
                name: "IX_Clubs_UserId",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Clubs");

            migrationBuilder.CreateTable(
                name: "ClubUser",
                columns: table => new
                {
                    UsersId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    clubsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubUser", x => new { x.UsersId, x.clubsId });
                    table.ForeignKey(
                        name: "FK_ClubUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubUser_Clubs_clubsId",
                        column: x => x.clubsId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ClubUser_clubsId",
                table: "ClubUser",
                column: "clubsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubUser");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Clubs",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_UserId",
                table: "Clubs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_AspNetUsers_UserId",
                table: "Clubs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
