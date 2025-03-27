﻿// <auto-generated />
using System;
using CustomCADs.Carts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CustomCADs.Carts.Persistence.Migrations
{
    [DbContext(typeof(CartsContext))]
    partial class CartsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Carts")
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CustomCADs.Carts.Domain.ActiveCarts.ActiveCart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("uuid")
                        .HasColumnName("BuyerId");

                    b.HasKey("Id");

                    b.ToTable("ActiveCarts", "Carts");
                });

            modelBuilder.Entity("CustomCADs.Carts.Domain.ActiveCarts.Entities.ActiveCartItem", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("ProductId");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uuid")
                        .HasColumnName("CartId");

                    b.Property<Guid?>("CustomizationId")
                        .HasColumnType("uuid")
                        .HasColumnName("CustomizationId");

                    b.Property<bool>("ForDelivery")
                        .HasColumnType("boolean")
                        .HasColumnName("ForDelivery");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("Quantity");

                    b.HasKey("ProductId");

                    b.HasIndex("CartId");

                    b.ToTable("ActiveCartItems", "Carts");
                });

            modelBuilder.Entity("CustomCADs.Carts.Domain.PurchasedCarts.Entities.PurchasedCartItem", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("ProductId");

                    b.Property<Guid>("CadId")
                        .HasColumnType("uuid")
                        .HasColumnName("CadId");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uuid")
                        .HasColumnName("CartId");

                    b.Property<Guid?>("CustomizationId")
                        .HasColumnType("uuid")
                        .HasColumnName("CustomizationId");

                    b.Property<bool>("ForDelivery")
                        .HasColumnType("boolean")
                        .HasColumnName("ForDelivery");

                    b.Property<decimal>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("Price");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("Quantity");

                    b.HasKey("ProductId");

                    b.HasIndex("CartId");

                    b.ToTable("PurchasedCartItems", "Carts");
                });

            modelBuilder.Entity("CustomCADs.Carts.Domain.PurchasedCarts.PurchasedCart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("uuid")
                        .HasColumnName("BuyerId");

                    b.Property<DateTimeOffset>("PurchasedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("PurchasedAt");

                    b.Property<Guid?>("ShipmentId")
                        .HasColumnType("uuid")
                        .HasColumnName("ShipmentId");

                    b.HasKey("Id");

                    b.ToTable("PurchasedCarts", "Carts");
                });

            modelBuilder.Entity("CustomCADs.Carts.Domain.ActiveCarts.Entities.ActiveCartItem", b =>
                {
                    b.HasOne("CustomCADs.Carts.Domain.ActiveCarts.ActiveCart", "Cart")
                        .WithMany("Items")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("CustomCADs.Carts.Domain.PurchasedCarts.Entities.PurchasedCartItem", b =>
                {
                    b.HasOne("CustomCADs.Carts.Domain.PurchasedCarts.PurchasedCart", "Cart")
                        .WithMany("Items")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("CustomCADs.Carts.Domain.ActiveCarts.ActiveCart", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("CustomCADs.Carts.Domain.PurchasedCarts.PurchasedCart", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
