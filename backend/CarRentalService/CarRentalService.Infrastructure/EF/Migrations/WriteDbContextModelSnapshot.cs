﻿// <auto-generated />
using System;
using CarRentalService.Infrastructure.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarRentalService.Infrastructure.EF.Migrations
{
    [DbContext(typeof(WriteDbContext))]
    partial class WriteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarRentalService.Domain.RentalPointAggregate.RentalPoint", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("RentalPoints");

                    b.HasData(
                        new
                        {
                            Id = new Guid("550e8400-e29b-41d4-a716-446655440100"),
                            Address = "Warsaw, Main St 1",
                            Name = "Warsaw Central"
                        },
                        new
                        {
                            Id = new Guid("550e8400-e29b-41d4-a716-446655440101"),
                            Address = "Katowice, Central Ave 10",
                            Name = "Katowice Station"
                        });
                });

            modelBuilder.Entity("CarRentalService.Domain.UserAggregate.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("CarRentalService.Domain.VehicleAggregate.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<bool>("IsAvailable")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("PricePerDay")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("RentalPointId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Seats")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Vehicles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d7b77389-46b1-4754-a3ae-e6533dfe6f50"),
                            Brand = "Toyota",
                            IsAvailable = true,
                            LicensePlate = "KR1234AB",
                            Model = "Camry",
                            PricePerDay = 50m,
                            RentalPointId = new Guid("550e8400-e29b-41d4-a716-446655440100"),
                            Seats = 5,
                            Type = "Car",
                            Year = 2020
                        },
                        new
                        {
                            Id = new Guid("c6a8cd83-b80b-4caa-99cc-958acd4a94fb"),
                            Brand = "Toyota",
                            IsAvailable = true,
                            LicensePlate = "KR7777AB",
                            Model = "Supra",
                            PricePerDay = 100m,
                            RentalPointId = new Guid("550e8400-e29b-41d4-a716-446655440100"),
                            Seats = 2,
                            Type = "Car",
                            Year = 2020
                        },
                        new
                        {
                            Id = new Guid("58cddf25-1a9f-453e-98c2-c753903adeb3"),
                            Brand = "Honda",
                            IsAvailable = true,
                            LicensePlate = "WA5678CD",
                            Model = "Civic",
                            PricePerDay = 45m,
                            RentalPointId = new Guid("550e8400-e29b-41d4-a716-446655440100"),
                            Seats = 5,
                            Type = "Car",
                            Year = 2021
                        },
                        new
                        {
                            Id = new Guid("4c423274-ec42-4079-ac5e-6f330c8a62ca"),
                            Brand = "Ford",
                            IsAvailable = true,
                            LicensePlate = "PO9012EF",
                            Model = "Focus",
                            PricePerDay = 40m,
                            RentalPointId = new Guid("550e8400-e29b-41d4-a716-446655440101"),
                            Seats = 4,
                            Type = "Car",
                            Year = 2019
                        },
                        new
                        {
                            Id = new Guid("b676eb66-d644-4045-8f0d-5411e2275535"),
                            Brand = "Ford",
                            IsAvailable = true,
                            LicensePlate = "PO4012FF",
                            Model = "F-150",
                            PricePerDay = 80m,
                            RentalPointId = new Guid("550e8400-e29b-41d4-a716-446655440101"),
                            Seats = 2,
                            Type = "Truck",
                            Year = 2018
                        },
                        new
                        {
                            Id = new Guid("c1494ae8-fb1d-4e31-9c89-b0d939e8f469"),
                            Brand = "Yamaha",
                            IsAvailable = true,
                            LicensePlate = "PO9014EL",
                            Model = "MT-07",
                            PricePerDay = 35m,
                            RentalPointId = new Guid("550e8400-e29b-41d4-a716-446655440101"),
                            Seats = 2,
                            Type = "Motorcycle",
                            Year = 2021
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<CarRentalService.Domain.UserAggregate.ValueObjects.UserId>", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<CarRentalService.Domain.UserAggregate.ValueObjects.UserId>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<CarRentalService.Domain.UserAggregate.ValueObjects.UserId>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<CarRentalService.Domain.UserAggregate.ValueObjects.UserId>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<CarRentalService.Domain.UserAggregate.ValueObjects.UserId>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<CarRentalService.Domain.UserAggregate.ValueObjects.UserId>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("CarRentalService.Domain.UserAggregate.User", b =>
                {
                    b.OwnsMany("CarRentalService.Domain.UserAggregate.Entities.RefreshToken", "RefreshTokens", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("AddedDate")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("datetime2")
                                .HasDefaultValueSql("GETUTCDATE()");

                            b1.Property<string>("ApplicationUserId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime>("CreatedAt")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("datetime2")
                                .HasDefaultValueSql("GETUTCDATE()");

                            b1.Property<DateTime>("ExpiryDate")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("datetime2")
                                .HasDefaultValueSql("GETUTCDATE()");

                            b1.Property<string>("JwtId")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Token")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("Id");

                            b1.HasIndex("ApplicationUserId");

                            b1.ToTable("RefreshToken");

                            b1.WithOwner()
                                .HasForeignKey("ApplicationUserId");
                        });

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("CarRentalService.Domain.VehicleAggregate.Vehicle", b =>
                {
                    b.OwnsMany("CarRentalService.Domain.VehicleAggregate.Entities.Reservation", "Reservations", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreatedAt")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("datetime2")
                                .HasDefaultValueSql("GETUTCDATE()");

                            b1.Property<DateTime>("EndDate")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("PickupPointId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("ReturnPointId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime?>("ReturnedDate")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime>("StartDate")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Status")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("UserId")
                                .IsRequired()
                                .HasColumnType("nvarchar(450)");

                            b1.Property<Guid>("VehicleId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Id");

                            b1.HasIndex("PickupPointId");

                            b1.HasIndex("ReturnPointId");

                            b1.HasIndex("UserId");

                            b1.HasIndex("VehicleId");

                            b1.ToTable("Reservations", (string)null);

                            b1.HasOne("CarRentalService.Domain.RentalPointAggregate.RentalPoint", null)
                                .WithMany()
                                .HasForeignKey("PickupPointId")
                                .OnDelete(DeleteBehavior.Restrict)
                                .IsRequired();

                            b1.HasOne("CarRentalService.Domain.RentalPointAggregate.RentalPoint", null)
                                .WithMany()
                                .HasForeignKey("ReturnPointId")
                                .OnDelete(DeleteBehavior.Restrict)
                                .IsRequired();

                            b1.HasOne("CarRentalService.Domain.UserAggregate.User", null)
                                .WithMany()
                                .HasForeignKey("UserId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("VehicleId");
                        });

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<CarRentalService.Domain.UserAggregate.ValueObjects.UserId>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<CarRentalService.Domain.UserAggregate.ValueObjects.UserId>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<CarRentalService.Domain.UserAggregate.ValueObjects.UserId>", b =>
                {
                    b.HasOne("CarRentalService.Domain.UserAggregate.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<CarRentalService.Domain.UserAggregate.ValueObjects.UserId>", b =>
                {
                    b.HasOne("CarRentalService.Domain.UserAggregate.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<CarRentalService.Domain.UserAggregate.ValueObjects.UserId>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<CarRentalService.Domain.UserAggregate.ValueObjects.UserId>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRentalService.Domain.UserAggregate.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<CarRentalService.Domain.UserAggregate.ValueObjects.UserId>", b =>
                {
                    b.HasOne("CarRentalService.Domain.UserAggregate.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
