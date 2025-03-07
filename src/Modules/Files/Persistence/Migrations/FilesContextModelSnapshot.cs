﻿// <auto-generated />
using System;
using System.Collections.Generic;
using CustomCADs.Files.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CustomCADs.Files.Persistence.Migrations
{
    [DbContext(typeof(FilesContext))]
    partial class FilesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Files")
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CustomCADs.Files.Domain.Cads.Cad", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ContentType");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Key");

                    b.Property<decimal>("Volume")
                        .HasColumnType("numeric")
                        .HasColumnName("Volume");

                    b.ComplexProperty<Dictionary<string, object>>("CamCoordinates", "CustomCADs.Files.Domain.Cads.Cad.CamCoordinates#Coordinates", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("X")
                                .HasColumnType("numeric")
                                .HasColumnName("CamX");

                            b1.Property<decimal>("Y")
                                .HasColumnType("numeric")
                                .HasColumnName("CamY");

                            b1.Property<decimal>("Z")
                                .HasColumnType("numeric")
                                .HasColumnName("CamZ");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PanCoordinates", "CustomCADs.Files.Domain.Cads.Cad.PanCoordinates#Coordinates", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("X")
                                .HasColumnType("numeric")
                                .HasColumnName("PanX");

                            b1.Property<decimal>("Y")
                                .HasColumnType("numeric")
                                .HasColumnName("PanY");

                            b1.Property<decimal>("Z")
                                .HasColumnType("numeric")
                                .HasColumnName("PanZ");
                        });

                    b.HasKey("Id");

                    b.ToTable("Cads", "Files");
                });

            modelBuilder.Entity("CustomCADs.Files.Domain.Images.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ContentType");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Key");

                    b.HasKey("Id");

                    b.ToTable("Images", "Files");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9a35cbea-806c-4561-ae71-bb21824f2432"),
                            ContentType = "image/webp",
                            Key = "textures/pla.webp"
                        },
                        new
                        {
                            Id = new Guid("bed27a31-107a-4b3f-a50a-cb9cc6f376f1"),
                            ContentType = "image/webp",
                            Key = "textures/abs.webp"
                        },
                        new
                        {
                            Id = new Guid("190a69a3-1b02-43f0-a4f9-cab22826abf3"),
                            ContentType = "image/webp",
                            Key = "textures/glow-in-dark.webp"
                        },
                        new
                        {
                            Id = new Guid("38deab9b-8791-4147-9958-64e9f7ec6d78"),
                            ContentType = "image/webp",
                            Key = "textures/tuf.webp"
                        },
                        new
                        {
                            Id = new Guid("3fe2472c-d2c6-434c-a013-ef117319bed3"),
                            ContentType = "image/webp",
                            Key = "textures/wood.webp"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
