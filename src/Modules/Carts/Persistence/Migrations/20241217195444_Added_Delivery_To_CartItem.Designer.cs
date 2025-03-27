﻿// <auto-generated />
using System;
using CustomCADs.Carts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CustomCADs.Carts.Persistence.Migrations
{
    [DbContext(typeof(CartsContext))]
    [Migration("20241217195444_Added_Delivery_To_CartItem")]
    partial class Added_Delivery_To_CartItem
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Carts")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CustomCADs.Carts.Domain.Carts.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("uuid")
                        .HasColumnName("BuyerId");

                    b.Property<DateTime>("PurchasedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("PurchasedAt");

                    b.Property<decimal>("Total")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)")
                        .HasColumnName("Total");

                    b.HasKey("Id");

                    b.ToTable("Carts", "Carts");
                });

            modelBuilder.Entity("CustomCADs.Carts.Domain.Carts.Entities.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CadId")
                        .HasColumnType("uuid")
                        .HasColumnName("CadId");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uuid")
                        .HasColumnName("CartId");

                    b.Property<bool>("Delivery")
                        .HasColumnType("boolean")
                        .HasColumnName("Delivery");

                    b.Property<decimal>("Price")
                        .HasPrecision(19, 2)
                        .HasColumnType("numeric(19,2)")
                        .HasColumnName("Price");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("ProductId");

                    b.Property<DateTime>("PurchasedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("PurchasedAt");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("Quantity");

                    b.Property<Guid?>("ShipmentId")
                        .HasColumnType("uuid")
                        .HasColumnName("ShipmentId");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.ToTable("CartItems", "Carts");
                });

            modelBuilder.Entity("CustomCADs.Carts.Domain.Carts.Entities.CartItem", b =>
                {
                    b.HasOne("CustomCADs.Carts.Domain.Carts.Cart", "Cart")
                        .WithMany("Items")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("CustomCADs.Carts.Domain.Carts.Cart", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
