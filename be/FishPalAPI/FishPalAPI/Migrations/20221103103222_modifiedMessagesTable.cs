using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FishPalAPI.Migrations
{
    public partial class modifiedMessagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatoruserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "MessageReceivers");

            migrationBuilder.RenameColumn(
                name: "InboxOutbox",
                table: "Messages",
                newName: "CreatorUserProfileId");

            migrationBuilder.AlterColumn<int>(
                name: "ApproverRequired",
                table: "Messages",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<int>(
                name: "AssignedUserProfileId",
                table: "MessageReceivers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedUserProfileId",
                table: "MessageReceivers");

            migrationBuilder.RenameColumn(
                name: "CreatorUserProfileId",
                table: "Messages",
                newName: "InboxOutbox");

            migrationBuilder.AlterColumn<Guid>(
                name: "ApproverRequired",
                table: "Messages",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatoruserId",
                table: "Messages",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "AssignedUserId",
                table: "MessageReceivers",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");
        }
    }
}
