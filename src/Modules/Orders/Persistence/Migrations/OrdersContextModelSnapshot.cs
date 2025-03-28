﻿// <auto-generated />
using System;
using CustomCADs.Orders.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CustomCADs.Orders.Persistence.Migrations
{
    [DbContext(typeof(OrdersContext))]
    partial class OrdersContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Orders")
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CustomCADs.Orders.Domain.CompletedOrders.CompletedOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("uuid")
                        .HasColumnName("BuyerId");

                    b.Property<Guid>("CadId")
                        .HasColumnType("uuid")
                        .HasColumnName("CadId");

                    b.Property<Guid?>("CustomizationId")
                        .HasColumnType("uuid")
                        .HasColumnName("CustomizationId");

                    b.Property<bool>("Delivery")
                        .HasColumnType("boolean")
                        .HasColumnName("Delivery");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("Description");

                    b.Property<Guid>("DesignerId")
                        .HasColumnType("uuid")
                        .HasColumnName("DesignerId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("Name");

                    b.Property<DateTimeOffset>("OrderedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("OrderedAt");

                    b.Property<decimal>("Price")
                        .HasPrecision(19, 2)
                        .HasColumnType("numeric(19,2)")
                        .HasColumnName("Price");

                    b.Property<DateTimeOffset>("PurchasedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("PurchasedAt");

                    b.Property<Guid?>("ShipmentId")
                        .HasColumnType("uuid")
                        .HasColumnName("ShipmentId");

                    b.HasKey("Id");

                    b.ToTable("CompletedOrders", "Orders");
                });

            modelBuilder.Entity("CustomCADs.Orders.Domain.OngoingOrders.OngoingOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("uuid")
                        .HasColumnName("BuyerId");

                    b.Property<Guid?>("CadId")
                        .HasColumnType("uuid")
                        .HasColumnName("CadId");

                    b.Property<bool>("Delivery")
                        .HasColumnType("boolean")
                        .HasColumnName("Delivery");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("Description");

                    b.Property<Guid?>("DesignerId")
                        .HasColumnType("uuid")
                        .HasColumnName("DesignerId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("Name");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("OrderStatus");

                    b.Property<DateTimeOffset>("OrderedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("OrderedAt");

                    b.Property<decimal?>("Price")
                        .HasPrecision(19, 2)
                        .HasColumnType("numeric(19,2)")
                        .HasColumnName("Price");

                    b.HasKey("Id");

                    b.ToTable("OngoingOrders", "Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
