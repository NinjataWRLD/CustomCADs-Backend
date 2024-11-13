﻿// <auto-generated />
using System;
using CustomCADs.Account.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CustomCADs.Account.Persistence.Migrations
{
    [DbContext(typeof(AccountContext))]
    [Migration("20241112141751_Set_User_Email_Max")]
    partial class Set_User_Email_Max
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Account")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CustomCADs.Account.Domain.Roles.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Roles", "Account");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Has access to Orders endpoints - can buy 3D Models from the Gallery and make and track Orders.",
                            Name = "Client"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Has access to Cads endpoints - can apply to upload his 3D Models to the Gallery, set their prices and track their status",
                            Name = "Contributor"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Has access to Cads and Designer endpoints - can upload his 3D Models straight to the Gallery, validate contributors' cads and finish clients' orders.",
                            Name = "Designer"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Has full access to Users, Roles, Orders, Cads, Categories and all other endpoints - can do anyhting.",
                            Name = "Administrator"
                        });
                });

            modelBuilder.Entity("CustomCADs.Account.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("nvarchar(320)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(62)
                        .HasColumnType("nvarchar(62)");

                    b.HasKey("Id");

                    b.HasIndex("RoleName");

                    b.ToTable("Users", "Account");
                });

            modelBuilder.Entity("CustomCADs.Account.Domain.Users.User", b =>
                {
                    b.HasOne("CustomCADs.Account.Domain.Roles.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleName")
                        .HasPrincipalKey("Name")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("CustomCADs.Account.Domain.Users.ValueObjects.Names", "Names", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FirstName")
                                .HasMaxLength(62)
                                .HasColumnType("nvarchar(62)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .HasMaxLength(62)
                                .HasColumnType("nvarchar(62)")
                                .HasColumnName("LastName");

                            b1.HasKey("UserId");

                            b1.ToTable("Users", "Account");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Names")
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CustomCADs.Account.Domain.Roles.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
