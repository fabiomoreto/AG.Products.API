﻿// <auto-generated />
using System;
using System.Collections.Generic;
using AG.Products.API.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AG.Products.API.Infra.Data.Migrations
{
    [DbContext(typeof(ProductContext))]
    [Migration("20240507202347_create_database")]
    partial class create_database
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AG.Products.API.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Code"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.ComplexProperty<Dictionary<string, object>>("Supplier", "AG.Products.API.Domain.Entities.Product.Supplier#Supplier", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("CNPJ")
                                .IsRequired()
                                .HasColumnType("varchar(18)")
                                .HasColumnName("CNPJ");

                            b1.Property<int>("Code")
                                .HasColumnType("int")
                                .HasColumnName("SupplierCode");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("varchar(100)")
                                .HasColumnName("SupplierName");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("ValidityPeriod", "AG.Products.API.Domain.Entities.Product.ValidityPeriod#ValidityPeriod", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<DateOnly>("DueDate")
                                .HasColumnType("date")
                                .HasColumnName("DueDate");

                            b1.Property<DateOnly>("ManufactureDate")
                                .HasColumnType("date")
                                .HasColumnName("ManufactureDate");
                        });

                    b.HasKey("Id");

                    b.ToTable("Products", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}