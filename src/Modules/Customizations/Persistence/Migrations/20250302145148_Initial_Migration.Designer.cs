﻿// <auto-generated />
using System;
using CustomCADs.Customizations.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CustomCADs.Customizations.Persistence.Migrations
{
    [DbContext(typeof(CustomizationsContext))]
    [Migration("20250302145148_Initial_Migration")]
    partial class Initial_Migration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Customizations")
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CustomCADs.Customizations.Domain.Customizations.Customization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)")
                        .HasColumnName("Color");

                    b.Property<decimal>("Infill")
                        .HasPrecision(4, 2)
                        .HasColumnType("numeric(4,2)")
                        .HasColumnName("Infill");

                    b.Property<int>("MaterialId")
                        .HasColumnType("integer")
                        .HasColumnName("MaterialId");

                    b.Property<decimal>("Scale")
                        .HasPrecision(4, 2)
                        .HasColumnType("numeric(4,2)")
                        .HasColumnName("Scale");

                    b.Property<decimal>("Volume")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)")
                        .HasColumnName("Volume");

                    b.HasKey("Id");

                    b.ToTable("Customizations", "Customizations");
                });

            modelBuilder.Entity("CustomCADs.Customizations.Domain.Materials.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cost")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)")
                        .HasColumnName("Cost");

                    b.Property<decimal>("Density")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)")
                        .HasColumnName("Density");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Name");

                    b.Property<Guid>("TextureId")
                        .HasColumnType("uuid")
                        .HasColumnName("TextureId");

                    b.HasKey("Id");

                    b.ToTable("Materials", "Customizations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cost = 30m,
                            Density = 1.24m,
                            Name = "PLA",
                            TextureId = new Guid("9a35cbea-806c-4561-ae71-bb21824f2432")
                        },
                        new
                        {
                            Id = 2,
                            Cost = 30m,
                            Density = 1.04m,
                            Name = "ABS",
                            TextureId = new Guid("bed27a31-107a-4b3f-a50a-cb9cc6f376f1")
                        },
                        new
                        {
                            Id = 3,
                            Cost = 30m,
                            Density = 1.25m,
                            Name = "Glow in dark",
                            TextureId = new Guid("190a69a3-1b02-43f0-a4f9-cab22826abf3")
                        },
                        new
                        {
                            Id = 4,
                            Cost = 30m,
                            Density = 1.27m,
                            Name = "TUF",
                            TextureId = new Guid("38deab9b-8791-4147-9958-64e9f7ec6d78")
                        },
                        new
                        {
                            Id = 5,
                            Cost = 30m,
                            Density = 1.23m,
                            Name = "Wood",
                            TextureId = new Guid("3fe2472c-d2c6-434c-a013-ef117319bed3")
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
