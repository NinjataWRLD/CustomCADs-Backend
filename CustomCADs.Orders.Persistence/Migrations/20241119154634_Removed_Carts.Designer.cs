﻿// <auto-generated />
using System;
using System.Collections.Generic;
using CustomCADs.Orders.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CustomCADs.Orders.Persistence.Migrations
{
    [DbContext(typeof(OrdersContext))]
    [Migration("20241119154634_Removed_Carts")]
    partial class Removed_Carts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Orders")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CustomCADs.Orders.Domain.CustomOrders.Entities.CustomOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("BuyerId");

                    b.Property<Guid?>("CadId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CadId");

                    b.Property<string>("DeliveryType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DeliveryType");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Description");

                    b.Property<Guid?>("DesignerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("DesignerId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Name");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("OrderDate");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("OrderStatus");

                    b.Property<Guid?>("ShipmentId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ShipmentId");

                    b.ComplexProperty<Dictionary<string, object>>("Image", "CustomCADs.Orders.Domain.CustomOrders.Entities.CustomOrder.Image#Image", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Path")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ImagePath");
                        });

                    b.HasKey("Id");

                    b.ToTable("CustomOrders", "Orders");
                });
#pragma warning restore 612, 618
        }
    }
}