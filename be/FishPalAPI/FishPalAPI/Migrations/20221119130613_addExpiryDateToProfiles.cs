using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace FishPalAPI.Migrations
{
    public partial class addExpiryDateToProfiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "expiryDate",
                table: "UserProfiles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "expiryDate",
                table: "UserProfiles");
        }
    }
}
