﻿// <auto-generated />
using System;
using ConcertApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConcertApp.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250107145838_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ConcertApp.Data.Entity.Booking", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PerformanceID")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PerformanceID");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings", (string)null);

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Email = "johndoe@example.com",
                            Name = "John's Rock Booking",
                            PerformanceID = 2,
                            UserId = 1
                        },
                        new
                        {
                            ID = 2,
                            Email = "janesmith@example.com",
                            Name = "Jane's Jazz Booking",
                            PerformanceID = 3,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("ConcertApp.Data.Entity.Concert", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("Concerts", (string)null);

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Description = "A night of amazing rock music.",
                            Name = "Rock Concert"
                        },
                        new
                        {
                            ID = 2,
                            Description = "Smooth jazz performances all evening.",
                            Name = "Jazz Night"
                        });
                });

            modelBuilder.Entity("ConcertApp.Data.Entity.Performance", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("ConcertId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasMaxLength(100)
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.HasIndex("ConcertId");

                    b.ToTable("Performances", (string)null);

                    b.HasData(
                        new
                        {
                            ID = 1,
                            ConcertId = 1,
                            DateTime = new DateTime(2025, 1, 7, 15, 58, 37, 940, DateTimeKind.Utc).AddTicks(7047),
                            Location = "Main Stage",
                            Name = "Opening Act"
                        },
                        new
                        {
                            ID = 2,
                            ConcertId = 1,
                            DateTime = new DateTime(2025, 1, 7, 17, 58, 37, 940, DateTimeKind.Utc).AddTicks(7548),
                            Location = "Main Stage",
                            Name = "Metallica"
                        },
                        new
                        {
                            ID = 3,
                            ConcertId = 2,
                            DateTime = new DateTime(2025, 1, 8, 16, 58, 37, 940, DateTimeKind.Utc).AddTicks(7552),
                            Location = "Jazz Club",
                            Name = "Jazz Ensemble"
                        });
                });

            modelBuilder.Entity("ConcertApp.Data.Entity.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            ID = 1,
                            email = "johndoe@example.com",
                            name = "John Doe",
                            password = "P@ssw0rd123"
                        },
                        new
                        {
                            ID = 2,
                            email = "janesmith@example.com",
                            name = "Jane Smith",
                            password = "Str0ngP@ssword!"
                        });
                });

            modelBuilder.Entity("ConcertApp.Data.Entity.Booking", b =>
                {
                    b.HasOne("ConcertApp.Data.Entity.Performance", "Performance")
                        .WithMany("Bookings")
                        .HasForeignKey("PerformanceID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ConcertApp.Data.Entity.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Performance");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ConcertApp.Data.Entity.Performance", b =>
                {
                    b.HasOne("ConcertApp.Data.Entity.Concert", "Concert")
                        .WithMany("Performances")
                        .HasForeignKey("ConcertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Concert");
                });

            modelBuilder.Entity("ConcertApp.Data.Entity.Concert", b =>
                {
                    b.Navigation("Performances");
                });

            modelBuilder.Entity("ConcertApp.Data.Entity.Performance", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("ConcertApp.Data.Entity.User", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
