using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FishPalAPI.Migrations
{
    public partial class addMedicalInformationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "medicalInformationId",
                table: "UserInformation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MedicalInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MedicalAidName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MedicalAidPlan = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MedicalAidNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MedicalAidContactNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalInformation", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MedicalInformationAllergies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AllergyName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AllergyReaction = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AllergyMedication = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MedicalInformationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalInformationAllergies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalInformationAllergies_MedicalInformation_MedicalInform~",
                        column: x => x.MedicalInformationId,
                        principalTable: "MedicalInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MedicalInformationEmergencyContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Relationship = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactNumberCell = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactNumberHome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MedicalInformationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalInformationEmergencyContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalInformationEmergencyContacts_MedicalInformation_Medic~",
                        column: x => x.MedicalInformationId,
                        principalTable: "MedicalInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MedicalInformationMedicalConditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ConditionName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MedicationName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MedicationDosage = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MedicationFrequency = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MedicalInformationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalInformationMedicalConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalInformationMedicalConditions_MedicalInformation_Medic~",
                        column: x => x.MedicalInformationId,
                        principalTable: "MedicalInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MedicalInformationPharmacies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PharmacyName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PharmacyContactNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MedicalInformationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalInformationPharmacies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalInformationPharmacies_MedicalInformation_MedicalInfor~",
                        column: x => x.MedicalInformationId,
                        principalTable: "MedicalInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MedicalInformationPhysicians",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PhysicianName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhysicianContactNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MedicalInformationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalInformationPhysicians", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalInformationPhysicians_MedicalInformation_MedicalInfor~",
                        column: x => x.MedicalInformationId,
                        principalTable: "MedicalInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserInformation_medicalInformationId",
                table: "UserInformation",
                column: "medicalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInformationAllergies_MedicalInformationId",
                table: "MedicalInformationAllergies",
                column: "MedicalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInformationEmergencyContacts_MedicalInformationId",
                table: "MedicalInformationEmergencyContacts",
                column: "MedicalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInformationMedicalConditions_MedicalInformationId",
                table: "MedicalInformationMedicalConditions",
                column: "MedicalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInformationPharmacies_MedicalInformationId",
                table: "MedicalInformationPharmacies",
                column: "MedicalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInformationPhysicians_MedicalInformationId",
                table: "MedicalInformationPhysicians",
                column: "MedicalInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInformation_MedicalInformation_medicalInformationId",
                table: "UserInformation",
                column: "medicalInformationId",
                principalTable: "MedicalInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInformation_MedicalInformation_medicalInformationId",
                table: "UserInformation");

            migrationBuilder.DropTable(
                name: "MedicalInformationAllergies");

            migrationBuilder.DropTable(
                name: "MedicalInformationEmergencyContacts");

            migrationBuilder.DropTable(
                name: "MedicalInformationMedicalConditions");

            migrationBuilder.DropTable(
                name: "MedicalInformationPharmacies");

            migrationBuilder.DropTable(
                name: "MedicalInformationPhysicians");

            migrationBuilder.DropTable(
                name: "MedicalInformation");

            migrationBuilder.DropIndex(
                name: "IX_UserInformation_medicalInformationId",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "medicalInformationId",
                table: "UserInformation");
        }
    }
}
