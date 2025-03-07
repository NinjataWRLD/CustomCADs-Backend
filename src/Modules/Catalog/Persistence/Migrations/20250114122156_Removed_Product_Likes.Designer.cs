﻿// <auto-generated />
using System;
using System.Collections.Generic;
using CustomCADs.Catalog.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CustomCADs.Catalog.Persistence.Migrations
{
    [DbContext(typeof(CatalogContext))]
    [Migration("20250114122156_Removed_Product_Likes")]
    partial class Removed_Product_Likes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Catalog")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CustomCADs.Catalog.Domain.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CadId")
                        .HasColumnType("uuid")
                        .HasColumnName("CadId");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("CategoryId");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid")
                        .HasColumnName("CreatorId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(750)
                        .HasColumnType("character varying(750)")
                        .HasColumnName("Description");

                    b.Property<Guid?>("DesignerId")
                        .HasColumnType("uuid")
                        .HasColumnName("DesignerId");

                    b.Property<Guid>("ImageId")
                        .HasColumnType("uuid")
                        .HasColumnName("ImageId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("character varying(18)")
                        .HasColumnName("Name");

                    b.Property<decimal>("Price")
                        .HasPrecision(19, 2)
                        .HasColumnType("numeric(19,2)")
                        .HasColumnName("Price");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Status");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UploadDate");

                    b.ComplexProperty<Dictionary<string, object>>("Counts", "CustomCADs.Catalog.Domain.Products.Product.Counts#Counts", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Purchases")
                                .HasColumnType("integer")
                                .HasColumnName("Purchases");

                            b1.Property<int>("Views")
                                .HasColumnType("integer")
                                .HasColumnName("Views");
                        });

                    b.HasKey("Id");

                    b.ToTable("Products", "Catalog");
                });
#pragma warning restore 612, 618
        }
    }
}
