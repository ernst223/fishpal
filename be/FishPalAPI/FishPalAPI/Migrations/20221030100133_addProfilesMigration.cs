using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FishPalAPI.Migrations
{
    public partial class addProfilesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Role_roleId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserInformation_userInformationId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ClubUser");

            migrationBuilder.DropTable(
                name: "FederationUser");

            migrationBuilder.RenameColumn(
                name: "userInformationId",
                table: "AspNetUsers",
                newName: "FederationId");

            migrationBuilder.RenameColumn(
                name: "roleId",
                table: "AspNetUsers",
                newName: "ClubId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_userInformationId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_FederationId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_roleId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_ClubId");

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    roleId = table.Column<int>(type: "int", nullable: true),
                    federationId = table.Column<int>(type: "int", nullable: true),
                    userInformationId = table.Column<int>(type: "int", nullable: true),
                    clubId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Clubs_clubId",
                        column: x => x.clubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Federation_federationId",
                        column: x => x.federationId,
                        principalTable: "Federation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Role_roleId",
                        column: x => x.roleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfiles_UserInformation_userInformationId",
                        column: x => x.userInformationId,
                        principalTable: "UserInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_clubId",
                table: "UserProfiles",
                column: "clubId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_federationId",
                table: "UserProfiles",
                column: "federationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_roleId",
                table: "UserProfiles",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_userInformationId",
                table: "UserProfiles",
                column: "userInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Clubs_ClubId",
                table: "AspNetUsers",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Federation_FederationId",
                table: "AspNetUsers",
                column: "FederationId",
                principalTable: "Federation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Clubs_ClubId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Federation_FederationId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.RenameColumn(
                name: "FederationId",
                table: "AspNetUsers",
                newName: "userInformationId");

            migrationBuilder.RenameColumn(
                name: "ClubId",
                table: "AspNetUsers",
                newName: "roleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_FederationId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_userInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_ClubId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_roleId");

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

            migrationBuilder.CreateTable(
                name: "FederationUser",
                columns: table => new
                {
                    federationsId = table.Column<int>(type: "int", nullable: false),
                    usersId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FederationUser", x => new { x.federationsId, x.usersId });
                    table.ForeignKey(
                        name: "FK_FederationUser_AspNetUsers_usersId",
                        column: x => x.usersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FederationUser_Federation_federationsId",
                        column: x => x.federationsId,
                        principalTable: "Federation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ClubUser_clubsId",
                table: "ClubUser",
                column: "clubsId");

            migrationBuilder.CreateIndex(
                name: "IX_FederationUser_usersId",
                table: "FederationUser",
                column: "usersId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Role_roleId",
                table: "AspNetUsers",
                column: "roleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserInformation_userInformationId",
                table: "AspNetUsers",
                column: "userInformationId",
                principalTable: "UserInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
