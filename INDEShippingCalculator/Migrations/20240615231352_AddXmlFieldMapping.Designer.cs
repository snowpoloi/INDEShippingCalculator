﻿// <auto-generated />
using System;
using INDEShipping.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace INDEShipping.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240615231352_AddXmlFieldMapping")]
    partial class AddXmlFieldMapping
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("INDEShipping.Models.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("BaseCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("CubicRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("ExtraCostDifficult")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("ExtraCostPerKg")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("MaxWeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("MinCharge")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("MinWeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TransportCompanyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransportCompanyId");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("INDEShipping.Models.PostalCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Area")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDifficultAccess")
                        .HasColumnType("bit");

                    b.Property<bool>("NoCOD")
                        .HasColumnType("bit");

                    b.Property<string>("Nomos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TransportCompanyId")
                        .HasColumnType("int");

                    b.Property<int?>("TransportCompanyId1")
                        .HasColumnType("int");

                    b.Property<int?>("TransportCompanyId2")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransportCompanyId");

                    b.HasIndex("TransportCompanyId1");

                    b.HasIndex("TransportCompanyId2");

                    b.ToTable("PostalCodes");
                });

            modelBuilder.Entity("INDEShipping.Models.TransportCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("MaxCubic")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MaxHeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MaxLength")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MaxWeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MaxWidth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OfferType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TransportCompanies");
                });

            modelBuilder.Entity("INDEShipping.Models.XmlFieldMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DatabaseField")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("XmlField")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("XmlFieldMappings");
                });

            modelBuilder.Entity("INDEShipping.Models.Offer", b =>
                {
                    b.HasOne("INDEShipping.Models.TransportCompany", "TransportCompany")
                        .WithMany()
                        .HasForeignKey("TransportCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TransportCompany");
                });

            modelBuilder.Entity("INDEShipping.Models.PostalCode", b =>
                {
                    b.HasOne("INDEShipping.Models.TransportCompany", null)
                        .WithMany("DifficultAreas")
                        .HasForeignKey("TransportCompanyId");

                    b.HasOne("INDEShipping.Models.TransportCompany", null)
                        .WithMany("NoCodAreas")
                        .HasForeignKey("TransportCompanyId1");

                    b.HasOne("INDEShipping.Models.TransportCompany", null)
                        .WithMany("ServicedAreas")
                        .HasForeignKey("TransportCompanyId2");
                });

            modelBuilder.Entity("INDEShipping.Models.TransportCompany", b =>
                {
                    b.Navigation("DifficultAreas");

                    b.Navigation("NoCodAreas");

                    b.Navigation("ServicedAreas");
                });
#pragma warning restore 612, 618
        }
    }
}
