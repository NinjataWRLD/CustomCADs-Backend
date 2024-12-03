﻿// <auto-generated />
using System;
using System.Collections.Generic;
using CustomCADs.Carts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CustomCADs.Carts.Persistence.Migrations
{
    [DbContext(typeof(CartsContext))]
    [Migration("20241119153147_Extracted_Gallery_From_Orders")]
    partial class Extracted_Gallery_From_Orders
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Gallery")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CustomCADs.Gallery.Domain.Carts.Entities.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("BuyerId");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("PurchaseDate");

                    b.Property<decimal>("Total")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Total");

                    b.HasKey("Id");

                    b.ToTable("Carts", "Gallery");
                });

            modelBuilder.Entity("CustomCADs.Gallery.Domain.Carts.Entities.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CadId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CadId");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CartId");

                    b.Property<string>("DeliveryType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ProductId");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("PurchaseDate");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("Quantity");

                    b.Property<Guid?>("ShipmentId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ShipmentId");

                    b.ComplexProperty<Dictionary<string, object>>("Price", "CustomCADs.Gallery.Domain.Carts.Entities.CartItem.Price#Money", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("PriceAmount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("PriceCurrency");

                            b1.Property<int>("Precision")
                                .HasColumnType("int")
                                .HasColumnName("PricePrecision");

                            b1.Property<string>("Symbol")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("PriceSymbol");
                        });

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.ToTable("CartItems", "Gallery");
                });

            modelBuilder.Entity("CustomCADs.Gallery.Domain.Carts.Entities.CartItem", b =>
                {
                    b.HasOne("CustomCADs.Gallery.Domain.Carts.Entities.Cart", "Cart")
                        .WithMany("Items")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("CustomCADs.Gallery.Domain.Carts.Entities.Cart", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
