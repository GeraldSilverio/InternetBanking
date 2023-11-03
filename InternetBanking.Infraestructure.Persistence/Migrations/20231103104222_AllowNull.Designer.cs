﻿// <auto-generated />
using System;
using InternetBanking.Infraestructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InternetBanking.Infraestructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20231103104222_AllowNull")]
    partial class AllowNull
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Beneficiary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountCode")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdBeneficiary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountCode")
                        .IsUnique();

                    b.ToTable("Benefiaciary", (string)null);
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.CreditsCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Available")
                        .HasPrecision(12, 2)
                        .HasColumnType("Decimal");

                    b.Property<int>("CardNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("CreditLimited")
                        .HasPrecision(12, 2)
                        .HasColumnType("Decimal");

                    b.Property<string>("IdUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Spent")
                        .HasPrecision(12, 2)
                        .HasColumnType("Decimal");

                    b.HasKey("Id");

                    b.HasIndex("CardNumber")
                        .IsUnique();

                    b.ToTable("CreditsCard", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
