﻿// <auto-generated />
using System;
using System.Collections.Generic;
using CustomCADs.Delivery.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CustomCADs.Delivery.Persistence.Migrations
{
    [DbContext(typeof(DeliveryContext))]
    [Migration("20241225112919_Persisted_Shipment_ReferenceId")]
    partial class Persisted_Shipment_ReferenceId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Delivery")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CustomCADs.Delivery.Domain.Shipments.Shipment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BuyerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("BuyerId");

                    b.Property<string>("ReferenceId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ReferenceId");

                    b.Property<string>("ShipmentStatus")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Status");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "CustomCADs.Delivery.Domain.Shipments.Shipment.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("City");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Country");
                        });

                    b.HasKey("Id");

                    b.ToTable("Shipments", "Delivery");
                });
#pragma warning restore 612, 618
        }
    }
}
