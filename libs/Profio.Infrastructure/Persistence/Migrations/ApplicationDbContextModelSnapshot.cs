﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Profio.Domain.ValueObjects;
using Profio.Infrastructure.Persistence;

#nullable disable

namespace Profio.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Profio.Domain.Entities.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(26)
                        .HasColumnType("character varying(26)");

                    b.Property<Address>("Address")
                        .IsUnicode(true)
                        .HasColumnType("jsonb");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int?>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character(10)")
                        .IsFixedLength();

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character(50)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Profio.Domain.Entities.DeliveryProgress", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(26)
                        .HasColumnType("character varying(26)");

                    b.Property<Location>("CurrentLocation")
                        .IsUnicode(true)
                        .HasColumnType("jsonb");

                    b.Property<string>("EstimatedTimeRemaining")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("OrderId")
                        .HasColumnType("character varying(26)");

                    b.Property<byte>("PercentComplete")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("DeliveryProgresses");
                });

            modelBuilder.Entity("Profio.Domain.Entities.Hub", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(26)
                        .HasColumnType("character varying(26)");

                    b.Property<Location>("Location")
                        .IsUnicode(true)
                        .HasColumnType("jsonb");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character(50)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.ToTable("Hubs");
                });

            modelBuilder.Entity("Profio.Domain.Entities.Incident", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(26)
                        .HasColumnType("character varying(26)");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .IsUnicode(true)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("OrderHistoryId")
                        .HasColumnType("character varying(26)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OrderHistoryId");

                    b.ToTable("Incidents");
                });

            modelBuilder.Entity("Profio.Domain.Entities.Order", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(26)
                        .HasColumnType("character varying(26)");

                    b.Property<string>("CustomerId")
                        .HasColumnType("character varying(26)");

                    b.Property<string>("DestinationZipCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<double?>("Distance")
                        .HasColumnType("double precision");

                    b.Property<DateTime?>("ExpectedDeliveryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("VehicleId")
                        .HasColumnType("character varying(26)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Profio.Domain.Entities.OrderHistory", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(26)
                        .HasColumnType("character varying(26)");

                    b.Property<string>("HubId")
                        .HasColumnType("character varying(26)");

                    b.Property<string>("OrderId")
                        .HasColumnType("character varying(26)");

                    b.Property<DateTime?>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("VehicleId")
                        .HasColumnType("character varying(26)");

                    b.HasKey("Id");

                    b.HasIndex("HubId");

                    b.HasIndex("OrderId");

                    b.HasIndex("VehicleId");

                    b.ToTable("OrderHistories");
                });

            modelBuilder.Entity("Profio.Domain.Entities.Route", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(26)
                        .HasColumnType("character varying(26)");

                    b.Property<double?>("Distance")
                        .HasColumnType("double precision");

                    b.Property<string>("EndHubId")
                        .HasColumnType("character varying(26)");

                    b.Property<string>("StartHubId")
                        .HasColumnType("character varying(26)");

                    b.HasKey("Id");

                    b.HasIndex("EndHubId");

                    b.HasIndex("StartHubId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("Profio.Domain.Entities.Staff", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(26)
                        .HasColumnType("character varying(26)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character(10)")
                        .IsFixedLength();

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("Profio.Domain.Entities.Vehicle", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(26)
                        .HasColumnType("character varying(26)");

                    b.Property<string>("LicensePlate")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("StaffId")
                        .HasColumnType("character varying(26)");

                    b.Property<int>("VehicleType")
                        .HasColumnType("integer");

                    b.Property<string>("ZipCodeCurrent")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("StaffId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Profio.Infrastructure.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Profio.Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Profio.Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Profio.Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Profio.Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Profio.Domain.Entities.DeliveryProgress", b =>
                {
                    b.HasOne("Profio.Domain.Entities.Order", "Order")
                        .WithMany("DeliveryProgresses")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Profio.Domain.Entities.Incident", b =>
                {
                    b.HasOne("Profio.Domain.Entities.OrderHistory", "OrderHistory")
                        .WithMany("Incidents")
                        .HasForeignKey("OrderHistoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("OrderHistory");
                });

            modelBuilder.Entity("Profio.Domain.Entities.Order", b =>
                {
                    b.HasOne("Profio.Domain.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Profio.Domain.Entities.Vehicle", "Vehicle")
                        .WithMany("Orders")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Customer");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Profio.Domain.Entities.OrderHistory", b =>
                {
                    b.HasOne("Profio.Domain.Entities.Hub", "Hub")
                        .WithMany("OrderHistories")
                        .HasForeignKey("HubId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Profio.Domain.Entities.Order", "Order")
                        .WithMany("OrderHistories")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Profio.Domain.Entities.Vehicle", "Vehicle")
                        .WithMany("OrderHistories")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Hub");

                    b.Navigation("Order");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Profio.Domain.Entities.Route", b =>
                {
                    b.HasOne("Profio.Domain.Entities.Hub", "EndHub")
                        .WithMany("EndRoutes")
                        .HasForeignKey("EndHubId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Profio.Domain.Entities.Hub", "StartHub")
                        .WithMany("StartRoutes")
                        .HasForeignKey("StartHubId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("EndHub");

                    b.Navigation("StartHub");
                });

            modelBuilder.Entity("Profio.Domain.Entities.Vehicle", b =>
                {
                    b.HasOne("Profio.Domain.Entities.Staff", "Staff")
                        .WithMany("Vehicles")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("Profio.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Profio.Domain.Entities.Hub", b =>
                {
                    b.Navigation("EndRoutes");

                    b.Navigation("OrderHistories");

                    b.Navigation("StartRoutes");
                });

            modelBuilder.Entity("Profio.Domain.Entities.Order", b =>
                {
                    b.Navigation("DeliveryProgresses");

                    b.Navigation("OrderHistories");
                });

            modelBuilder.Entity("Profio.Domain.Entities.OrderHistory", b =>
                {
                    b.Navigation("Incidents");
                });

            modelBuilder.Entity("Profio.Domain.Entities.Staff", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("Profio.Domain.Entities.Vehicle", b =>
                {
                    b.Navigation("OrderHistories");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
