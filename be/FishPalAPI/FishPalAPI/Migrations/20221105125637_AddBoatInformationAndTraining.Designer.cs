﻿// <auto-generated />
using System;
using FishPalAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FishPalAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221105125637_AddBoatInformationAndTraining")]
    partial class AddBoatInformationAndTraining
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("FacetProvince", b =>
                {
                    b.Property<int>("FacetsId")
                        .HasColumnType("int");

                    b.Property<int>("ProvincesId")
                        .HasColumnType("int");

                    b.HasKey("FacetsId", "ProvincesId");

                    b.HasIndex("ProvincesId");

                    b.ToTable("FacetProvince");
                });

            modelBuilder.Entity("FishPalAPI.Data.Club", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("FacetId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int?>("ProvinceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FacetId");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("FishPalAPI.Data.Communication.MessageReceivers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AssignedUserProfileId")
                        .HasColumnType("int");

                    b.Property<int>("MessagesFKId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MessagesFKId");

                    b.ToTable("MessageReceivers");
                });

            modelBuilder.Entity("FishPalAPI.Data.Communication.Messages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ApproverRequired")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatorUserProfileId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StatusChangeDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("FishPalAPI.Data.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AprovalDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Aproved")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Note")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("FishPalAPI.Data.DocumentMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("DocumentId")
                        .HasColumnType("int");

                    b.Property<int?>("RecipientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("RecipientId");

                    b.ToTable("DocumentMessages");
                });

            modelBuilder.Entity("FishPalAPI.Data.Facet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Base64String")
                        .HasColumnType("longtext");

                    b.Property<string>("Federation")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Facets");
                });

            modelBuilder.Entity("FishPalAPI.Data.Federation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Federation");
                });

            modelBuilder.Entity("FishPalAPI.Data.MedicalInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("MedicalAidContactNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("MedicalAidName")
                        .HasColumnType("longtext");

                    b.Property<string>("MedicalAidNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("MedicalAidPlan")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("MedicalInformation");
                });

            modelBuilder.Entity("FishPalAPI.Data.MedicalInformationAllergies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AllergyMedication")
                        .HasColumnType("longtext");

                    b.Property<string>("AllergyName")
                        .HasColumnType("longtext");

                    b.Property<string>("AllergyReaction")
                        .HasColumnType("longtext");

                    b.Property<int?>("MedicalInformationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MedicalInformationId");

                    b.ToTable("MedicalInformationAllergies");
                });

            modelBuilder.Entity("FishPalAPI.Data.MedicalInformationEmergencyContacts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ContactNumberCell")
                        .HasColumnType("longtext");

                    b.Property<string>("ContactNumberHome")
                        .HasColumnType("longtext");

                    b.Property<int?>("MedicalInformationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Relationship")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("MedicalInformationId");

                    b.ToTable("MedicalInformationEmergencyContacts");
                });

            modelBuilder.Entity("FishPalAPI.Data.MedicalInformationMedicalConditions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ConditionName")
                        .HasColumnType("longtext");

                    b.Property<int?>("MedicalInformationId")
                        .HasColumnType("int");

                    b.Property<string>("MedicationDosage")
                        .HasColumnType("longtext");

                    b.Property<string>("MedicationFrequency")
                        .HasColumnType("longtext");

                    b.Property<string>("MedicationName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("MedicalInformationId");

                    b.ToTable("MedicalInformationMedicalConditions");
                });

            modelBuilder.Entity("FishPalAPI.Data.MedicalInformationPharmacies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("MedicalInformationId")
                        .HasColumnType("int");

                    b.Property<string>("PharmacyContactNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("PharmacyName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("MedicalInformationId");

                    b.ToTable("MedicalInformationPharmacies");
                });

            modelBuilder.Entity("FishPalAPI.Data.MedicalInformationPhysicians", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("MedicalInformationId")
                        .HasColumnType("int");

                    b.Property<string>("PhysicianContactNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("PhysicianName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("MedicalInformationId");

                    b.ToTable("MedicalInformationPhysicians");
                });

            modelBuilder.Entity("FishPalAPI.Data.Member_Information.Boat_Information.BoatInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BoatNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("BoatOwner")
                        .HasColumnType("longtext");

                    b.Property<string>("CofExpiryDate")
                        .HasColumnType("longtext");

                    b.Property<string>("CofNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("HorsePower")
                        .HasColumnType("longtext");

                    b.Property<string>("HullColour")
                        .HasColumnType("longtext");

                    b.Property<string>("HullType")
                        .HasColumnType("longtext");

                    b.Property<string>("MotorMake")
                        .HasColumnType("longtext");

                    b.Property<string>("TowVehicleRegistrationNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("TrailerRegistrationNumber")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("BoatInformation");
                });

            modelBuilder.Entity("FishPalAPI.Data.Member_Information.Geo_Province_Information.GeoProvinceInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("GeoProvince")
                        .HasColumnType("longtext");

                    b.Property<string>("Position")
                        .HasColumnType("longtext");

                    b.Property<string>("ProvincialSasaccManagement")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("GeoProvinecInformation");
                });

            modelBuilder.Entity("FishPalAPI.Data.Member_Information.Training.Training", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AdminPointsReceived")
                        .HasColumnType("longtext");

                    b.Property<string>("AdminYearCompleted")
                        .HasColumnType("longtext");

                    b.Property<string>("CaptainPointsReceived")
                        .HasColumnType("longtext");

                    b.Property<string>("CaptainYearCompleted")
                        .HasColumnType("longtext");

                    b.Property<string>("CoachLvl1PointsReceived")
                        .HasColumnType("longtext");

                    b.Property<string>("CoachLvl1YearCompleted")
                        .HasColumnType("longtext");

                    b.Property<string>("CoachLvl2PointsReceived")
                        .HasColumnType("longtext");

                    b.Property<string>("CoachLvl2YearCompleted")
                        .HasColumnType("longtext");

                    b.Property<string>("ManagerPointsReceived")
                        .HasColumnType("longtext");

                    b.Property<string>("ManagerYearCompleted")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Training");
                });

            modelBuilder.Entity("FishPalAPI.Data.OrderItems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Amount")
                        .HasColumnType("double");

                    b.Property<string>("CategoryName")
                        .HasColumnType("longtext");

                    b.Property<string>("ItemName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("FishPalAPI.Data.PersonalInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("cell")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("dob")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ethnicGroup")
                        .HasColumnType("longtext");

                    b.Property<string>("gender")
                        .HasColumnType("longtext");

                    b.Property<string>("homeAddress")
                        .HasColumnType("longtext");

                    b.Property<string>("idNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("nationality")
                        .HasColumnType("longtext");

                    b.Property<string>("nickName")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("passportExpirationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("passportNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("phone")
                        .HasColumnType("longtext");

                    b.Property<string>("postalAddress")
                        .HasColumnType("longtext");

                    b.Property<string>("skipperLicenseNumber")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("PersonalInformation");
                });

            modelBuilder.Entity("FishPalAPI.Data.Province", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("FishPalAPI.Data.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("FishPalAPI.Data.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int?>("ClubId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("FederationId")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("Surname")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.HasIndex("FederationId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("FishPalAPI.Data.UserInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("boatInformationId")
                        .HasColumnType("int");

                    b.Property<int?>("geoProvinceInformationId")
                        .HasColumnType("int");

                    b.Property<int?>("medicalInformationId")
                        .HasColumnType("int");

                    b.Property<int?>("personalInformationId")
                        .HasColumnType("int");

                    b.Property<int?>("trainingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("boatInformationId");

                    b.HasIndex("geoProvinceInformationId");

                    b.HasIndex("medicalInformationId");

                    b.HasIndex("personalInformationId");

                    b.HasIndex("trainingId");

                    b.ToTable("UserInformation");
                });

            modelBuilder.Entity("FishPalAPI.Data.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("clubId")
                        .HasColumnType("int");

                    b.Property<DateTime>("creationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("roleId")
                        .HasColumnType("int");

                    b.Property<int?>("userInformationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("clubId");

                    b.HasIndex("roleId");

                    b.HasIndex("userInformationId");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FacetProvince", b =>
                {
                    b.HasOne("FishPalAPI.Data.Facet", null)
                        .WithMany()
                        .HasForeignKey("FacetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FishPalAPI.Data.Province", null)
                        .WithMany()
                        .HasForeignKey("ProvincesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FishPalAPI.Data.Club", b =>
                {
                    b.HasOne("FishPalAPI.Data.Facet", "Facet")
                        .WithMany()
                        .HasForeignKey("FacetId");

                    b.HasOne("FishPalAPI.Data.Province", "Province")
                        .WithMany()
                        .HasForeignKey("ProvinceId");

                    b.Navigation("Facet");

                    b.Navigation("Province");
                });

            modelBuilder.Entity("FishPalAPI.Data.Communication.MessageReceivers", b =>
                {
                    b.HasOne("FishPalAPI.Data.Communication.Messages", "Messages")
                        .WithMany("AssignedUsers")
                        .HasForeignKey("MessagesFKId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("FishPalAPI.Data.Document", b =>
                {
                    b.HasOne("FishPalAPI.Data.UserProfile", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("FishPalAPI.Data.DocumentMessage", b =>
                {
                    b.HasOne("FishPalAPI.Data.Document", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId");

                    b.HasOne("FishPalAPI.Data.UserProfile", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId");

                    b.Navigation("Document");

                    b.Navigation("Recipient");
                });

            modelBuilder.Entity("FishPalAPI.Data.MedicalInformationAllergies", b =>
                {
                    b.HasOne("FishPalAPI.Data.MedicalInformation", null)
                        .WithMany("MedicalInformationAllergies")
                        .HasForeignKey("MedicalInformationId");
                });

            modelBuilder.Entity("FishPalAPI.Data.MedicalInformationEmergencyContacts", b =>
                {
                    b.HasOne("FishPalAPI.Data.MedicalInformation", null)
                        .WithMany("MedicalInformationEmergencyContacts")
                        .HasForeignKey("MedicalInformationId");
                });

            modelBuilder.Entity("FishPalAPI.Data.MedicalInformationMedicalConditions", b =>
                {
                    b.HasOne("FishPalAPI.Data.MedicalInformation", null)
                        .WithMany("MedicalInformationMedicalConditions")
                        .HasForeignKey("MedicalInformationId");
                });

            modelBuilder.Entity("FishPalAPI.Data.MedicalInformationPharmacies", b =>
                {
                    b.HasOne("FishPalAPI.Data.MedicalInformation", null)
                        .WithMany("MedicalInformationPharmacies")
                        .HasForeignKey("MedicalInformationId");
                });

            modelBuilder.Entity("FishPalAPI.Data.MedicalInformationPhysicians", b =>
                {
                    b.HasOne("FishPalAPI.Data.MedicalInformation", null)
                        .WithMany("MedicalInformationPhysicians")
                        .HasForeignKey("MedicalInformationId");
                });

            modelBuilder.Entity("FishPalAPI.Data.User", b =>
                {
                    b.HasOne("FishPalAPI.Data.Club", null)
                        .WithMany("Users")
                        .HasForeignKey("ClubId");

                    b.HasOne("FishPalAPI.Data.Federation", null)
                        .WithMany("users")
                        .HasForeignKey("FederationId");
                });

            modelBuilder.Entity("FishPalAPI.Data.UserInformation", b =>
                {
                    b.HasOne("FishPalAPI.Data.Member_Information.Boat_Information.BoatInformation", "boatInformation")
                        .WithMany()
                        .HasForeignKey("boatInformationId");

                    b.HasOne("FishPalAPI.Data.Member_Information.Geo_Province_Information.GeoProvinceInformation", "geoProvinceInformation")
                        .WithMany()
                        .HasForeignKey("geoProvinceInformationId");

                    b.HasOne("FishPalAPI.Data.MedicalInformation", "medicalInformation")
                        .WithMany()
                        .HasForeignKey("medicalInformationId");

                    b.HasOne("FishPalAPI.Data.PersonalInformation", "personalInformation")
                        .WithMany()
                        .HasForeignKey("personalInformationId");

                    b.HasOne("FishPalAPI.Data.Member_Information.Training.Training", "training")
                        .WithMany()
                        .HasForeignKey("trainingId");

                    b.Navigation("boatInformation");

                    b.Navigation("geoProvinceInformation");

                    b.Navigation("medicalInformation");

                    b.Navigation("personalInformation");

                    b.Navigation("training");
                });

            modelBuilder.Entity("FishPalAPI.Data.UserProfile", b =>
                {
                    b.HasOne("FishPalAPI.Data.User", null)
                        .WithMany("profiles")
                        .HasForeignKey("UserId");

                    b.HasOne("FishPalAPI.Data.Club", "club")
                        .WithMany()
                        .HasForeignKey("clubId");

                    b.HasOne("FishPalAPI.Data.Role", "role")
                        .WithMany()
                        .HasForeignKey("roleId");

                    b.HasOne("FishPalAPI.Data.UserInformation", "userInformation")
                        .WithMany()
                        .HasForeignKey("userInformationId");

                    b.Navigation("club");

                    b.Navigation("role");

                    b.Navigation("userInformation");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FishPalAPI.Data.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FishPalAPI.Data.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FishPalAPI.Data.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FishPalAPI.Data.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FishPalAPI.Data.Club", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("FishPalAPI.Data.Communication.Messages", b =>
                {
                    b.Navigation("AssignedUsers");
                });

            modelBuilder.Entity("FishPalAPI.Data.Federation", b =>
                {
                    b.Navigation("users");
                });

            modelBuilder.Entity("FishPalAPI.Data.MedicalInformation", b =>
                {
                    b.Navigation("MedicalInformationAllergies");

                    b.Navigation("MedicalInformationEmergencyContacts");

                    b.Navigation("MedicalInformationMedicalConditions");

                    b.Navigation("MedicalInformationPharmacies");

                    b.Navigation("MedicalInformationPhysicians");
                });

            modelBuilder.Entity("FishPalAPI.Data.User", b =>
                {
                    b.Navigation("profiles");
                });
#pragma warning restore 612, 618
        }
    }
}
