﻿// <auto-generated />
using System;
using CustomCADs.Identity.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CustomCADs.Identity.Infrastructure.Migrations
{
    [DbContext(typeof(IdentityContext))]
    [Migration("20241130180115_Seeded_Users")]
    partial class Seeded_Users
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Auth")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CustomCADs.Auth.Domain.Entities.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", "Auth");

                    b.HasData(
                        new
                        {
                            Id = new Guid("762ddec2-25c9-4183-9891-72a19d84a839"),
                            ConcurrencyStamp = "51da1b9f-803c-4bd3-9a00-da7ac259ce32",
                            Name = "Client",
                            NormalizedName = "CLIENT"
                        },
                        new
                        {
                            Id = new Guid("e1101e2c-32cc-456f-9c82-4f1d1a65d141"),
                            ConcurrencyStamp = "a1a170e0-ee84-4afe-afd9-1df57009f291",
                            Name = "Contributor",
                            NormalizedName = "CONTRIBUTOR"
                        },
                        new
                        {
                            Id = new Guid("f3ad41d3-ee90-4988-9195-8b2a8f4f2733"),
                            ConcurrencyStamp = "1a8ba0a7-4853-42da-980d-3107784e7ab1",
                            Name = "Designer",
                            NormalizedName = "DESIGNER"
                        },
                        new
                        {
                            Id = new Guid("fad1b19d-5333-4633-bd84-d67c64649f65"),
                            ConcurrencyStamp = "42174679-32f1-48b0-9524-0f00791ec760",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        });
                });

            modelBuilder.Entity("CustomCADs.Auth.Domain.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", "Auth");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e38c495f-b1f3-4226-d289-08dd11623eb9"),
                            AccessFailedCount = 0,
                            AccountId = new Guid("2da61b05-1a27-4af9-9df2-be4f1f4e835f"),
                            ConcurrencyStamp = "0c5bbfb2-d132-407b-9b1b-e1e640ccc14e",
                            Email = "ivanzlatinov006@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = true,
                            NormalizedEmail = "IVANZLATINOV006@GMAIL.COM",
                            NormalizedUserName = "FOR7A7A",
                            PasswordHash = "AQAAAAIAAYagAAAAEMxKo17QTeytzknDR27c10aVDBF1wGzycD+CSTbVliUg0h8g6f4U2AAQTh9YAoPXYw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "3A6TFN6VVZNRZEG22J777XJTPQY7342B",
                            TwoFactorEnabled = false,
                            UserName = "For7a7a"
                        },
                        new
                        {
                            Id = new Guid("af840410-f3f2-4a3b-d28a-08dd11623eb9"),
                            AccessFailedCount = 0,
                            AccountId = new Guid("6d963818-23dc-4e9a-aaa8-b4c77252bc97"),
                            ConcurrencyStamp = "c77927de-61e7-4d53-be8d-a5390fafc75c",
                            Email = "PDMatsaliev20@codingburgas.bg",
                            EmailConfirmed = true,
                            LockoutEnabled = true,
                            NormalizedEmail = "PDMATSALIEV20@CODINGBURGAS.BG",
                            NormalizedUserName = "PETARDMATSALIEV",
                            PasswordHash = "AQAAAAIAAYagAAAAEJNqqiC31XGVrNSSflDLpuNzs/PIzg8VXCyEOL2hqvWAYi8a37bn5CUxHdvVuszSsQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "NWGZ3JTQSDNS346DMU7RP4IT4BDLHIQC",
                            TwoFactorEnabled = false,
                            UserName = "PetarDMatsaliev"
                        },
                        new
                        {
                            Id = new Guid("4337a774-2c5c-4c27-d28b-08dd11623eb9"),
                            AccessFailedCount = 0,
                            AccountId = new Guid("0fb3212f-7d51-4586-8fc2-0f333ec9fbc1"),
                            ConcurrencyStamp = "c5940d6f-d5c0-4f84-a262-da9b07525c3c",
                            Email = "boriskolev2006@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = true,
                            NormalizedEmail = "BORISKOLEV2006@GMAIL.COM",
                            NormalizedUserName = "ORACLE3000",
                            PasswordHash = "AQAAAAIAAYagAAAAEIuVU3Ziopa1Dv4t79ImAnluJSpVuJpvQawEaF/11u9szawuOWYd5yErqFGevwRHwg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "FNNIT3NPOZKZK2E67WFLV5R3RGVBX7LV",
                            TwoFactorEnabled = false,
                            UserName = "Oracle3000"
                        },
                        new
                        {
                            Id = new Guid("cb7749fb-3fff-4902-d28c-08dd11623eb9"),
                            AccessFailedCount = 0,
                            AccountId = new Guid("e995039c-a535-4f20-8288-7aadcb71b252"),
                            ConcurrencyStamp = "5c94b43f-861c-4efa-a670-5627e49d354d",
                            Email = "ivanangelov414@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = true,
                            NormalizedEmail = "IVANANGELOV414@GMAIL.COM",
                            NormalizedUserName = "NINJATABG",
                            PasswordHash = "AQAAAAIAAYagAAAAEI/R+FhQaDs57q+Z94HwbWVhv8PXnUlhXb71NicOb2CQPwTgdN9C1bRsRAIsfijjsA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "YIA26UZDSN2V2U5PVDEK4F3EJS3P5D3X",
                            TwoFactorEnabled = false,
                            UserName = "NinjataBG"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", "Auth");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", "Auth");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", "Auth");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", "Auth");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("e38c495f-b1f3-4226-d289-08dd11623eb9"),
                            RoleId = new Guid("762ddec2-25c9-4183-9891-72a19d84a839")
                        },
                        new
                        {
                            UserId = new Guid("af840410-f3f2-4a3b-d28a-08dd11623eb9"),
                            RoleId = new Guid("e1101e2c-32cc-456f-9c82-4f1d1a65d141")
                        },
                        new
                        {
                            UserId = new Guid("4337a774-2c5c-4c27-d28b-08dd11623eb9"),
                            RoleId = new Guid("f3ad41d3-ee90-4988-9195-8b2a8f4f2733")
                        },
                        new
                        {
                            UserId = new Guid("cb7749fb-3fff-4902-d28c-08dd11623eb9"),
                            RoleId = new Guid("fad1b19d-5333-4633-bd84-d67c64649f65")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", "Auth");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("CustomCADs.Auth.Domain.Entities.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("CustomCADs.Auth.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("CustomCADs.Auth.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("CustomCADs.Auth.Domain.Entities.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CustomCADs.Auth.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("CustomCADs.Auth.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}