using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FishPalAPI.Migrations
{
    public partial class changeMessageTableChangedToApproverRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalLevelRequired",
                table: "Messages");

            migrationBuilder.AddColumn<Guid>(
                name: "ApproverRequired",
                table: "Messages",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproverRequired",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "ApprovalLevelRequired",
                table: "Messages",
                type: "int",
                nullable: true);
        }
    }
}
