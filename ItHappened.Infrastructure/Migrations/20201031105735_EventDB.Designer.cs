﻿// <auto-generated />
using System;
using ItHappened.Infrastructure.EFCoreRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ItHappened.Infrastructure.Migrations
{
    [DbContext(typeof(ItHappenedDbContext))]
    [Migration("20201031105735_EventDB")]
    partial class EventDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ItHappened.Domain.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ItHappened.Domain.GeoTag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("GpsLat")
                        .HasColumnType("float");

                    b.Property<double>("GpsLng")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("GeoTags");
                });

            modelBuilder.Entity("ItHappened.Domain.Photo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("PhotoBytes")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("ItHappened.Infrastructure.EFCoreRepositories.EventDb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("HappensDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<double?>("Rating")
                        .HasColumnType("float");

                    b.Property<double?>("Scale")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("EventRequests");
                });

            modelBuilder.Entity("ItHappened.Infrastructure.EFCoreRepositories.GeoTagDb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventDbId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("GpsLat")
                        .HasColumnType("float");

                    b.Property<double>("GpsLng")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("EventDbId")
                        .IsUnique();

                    b.ToTable("GeoTagDb");
                });

            modelBuilder.Entity("ItHappened.Infrastructure.EFCoreRepositories.GeoTagDb", b =>
                {
                    b.HasOne("ItHappened.Infrastructure.EFCoreRepositories.EventDb", "EventDb")
                        .WithOne("GeoTag")
                        .HasForeignKey("ItHappened.Infrastructure.EFCoreRepositories.GeoTagDb", "EventDbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}