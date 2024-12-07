﻿// <auto-generated />
using System;
using System.Collections.Generic;
using CustomCADs.Accounts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CustomCADs.Accounts.Persistence.Migrations
{
    [DbContext(typeof(AccountsContext))]
    [Migration("20241127204230_Added_Unique_Indexes")]
    partial class Added_Unique_Indexes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Account")
                .HasAnnotation("ProductVersion", "9.0.0")
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
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Name");

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
                        .HasColumnType("nvarchar(320)")
                        .HasColumnName("Email");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("RoleName");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(62)
                        .HasColumnType("nvarchar(62)")
                        .HasColumnName("Username");

                    b.ComplexProperty<Dictionary<string, object>>("Names", "CustomCADs.Account.Domain.Users.User.Names#Names", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .HasMaxLength(62)
                                .HasColumnType("nvarchar(62)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .HasMaxLength(62)
                                .HasColumnType("nvarchar(62)")
                                .HasColumnName("LastName");
                        });

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleName");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users", "Account");
                });

            modelBuilder.Entity("CustomCADs.Account.Domain.Users.User", b =>
                {
                    b.HasOne("CustomCADs.Account.Domain.Roles.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleName")
                        .HasPrincipalKey("Name")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
