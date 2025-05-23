﻿// <auto-generated />
using CustomCADs.Categories.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CustomCADs.Categories.Persistence.Migrations
{
    [DbContext(typeof(CategoriesContext))]
    partial class CategoriesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Categories")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CustomCADs.Categories.Domain.Categories.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories", "Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Includes pets, wild animals, etc.",
                            Name = "Animals"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Includes movie characters, book characters, game characters, etc.",
                            Name = "Characters"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Includes phones, computers, e-devices, earphones, etc.",
                            Name = "Electronics"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Includes clothes, shoes, accessories, hats, etc.",
                            Name = "Fashion"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Includes tables, chairs, beds, etc.",
                            Name = "Furniture"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Includes flowers, forests, seas, etc.",
                            Name = "Nature"
                        },
                        new
                        {
                            Id = 7,
                            Description = "Includes organs, tools, chemical fluids, etc.",
                            Name = "Science"
                        },
                        new
                        {
                            Id = 8,
                            Description = "Includes footballs, boxing gloves, hockey sticks, etc.",
                            Name = "Sports"
                        },
                        new
                        {
                            Id = 9,
                            Description = "Includes pet toys, action figures, plushies, etc.",
                            Name = "Toys"
                        },
                        new
                        {
                            Id = 10,
                            Description = "Includes cars, trucks, tanks, bikes, planes, ships, etc.",
                            Name = "Vehicles"
                        },
                        new
                        {
                            Id = 11,
                            Description = "Includes anything that doesn't fit into the other categories.",
                            Name = "Others"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
