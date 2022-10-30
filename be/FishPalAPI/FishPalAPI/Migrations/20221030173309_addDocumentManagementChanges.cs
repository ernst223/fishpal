using Microsoft.EntityFrameworkCore.Migrations;

namespace FishPalAPI.Migrations
{
    public partial class addDocumentManagementChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentMessages_Documents_DocumentSendId",
                table: "DocumentMessages");

            migrationBuilder.DropColumn(
                name: "SendFrom",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "SendFrom",
                table: "DocumentMessages");

            migrationBuilder.DropColumn(
                name: "SendTo",
                table: "DocumentMessages");

            migrationBuilder.RenameColumn(
                name: "DocumentSendId",
                table: "DocumentMessages",
                newName: "RecipientId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentMessages_DocumentSendId",
                table: "DocumentMessages",
                newName: "IX_DocumentMessages_RecipientId");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Documents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "DocumentMessages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CreatedById",
                table: "Documents",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentMessages_DocumentId",
                table: "DocumentMessages",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentMessages_Documents_DocumentId",
                table: "DocumentMessages",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentMessages_UserProfiles_RecipientId",
                table: "DocumentMessages",
                column: "RecipientId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_UserProfiles_CreatedById",
                table: "Documents",
                column: "CreatedById",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentMessages_Documents_DocumentId",
                table: "DocumentMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentMessages_UserProfiles_RecipientId",
                table: "DocumentMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_UserProfiles_CreatedById",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_CreatedById",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_DocumentMessages_DocumentId",
                table: "DocumentMessages");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "DocumentMessages");

            migrationBuilder.RenameColumn(
                name: "RecipientId",
                table: "DocumentMessages",
                newName: "DocumentSendId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentMessages_RecipientId",
                table: "DocumentMessages",
                newName: "IX_DocumentMessages_DocumentSendId");

            migrationBuilder.AddColumn<string>(
                name: "SendFrom",
                table: "Documents",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SendFrom",
                table: "DocumentMessages",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SendTo",
                table: "DocumentMessages",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentMessages_Documents_DocumentSendId",
                table: "DocumentMessages",
                column: "DocumentSendId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
