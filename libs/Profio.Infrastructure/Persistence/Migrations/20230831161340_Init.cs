using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Profio.Domain.ValueObjects;

#nullable disable

namespace Profio.Infrastructure.Persistence.Migrations
{
  /// <inheritdoc />
  public partial class Init : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "AspNetRoles",
          columns: table => new
          {
            Id = table.Column<string>(type: "text", nullable: false),
            Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
            NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
            ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_AspNetRoles", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "AspNetUsers",
          columns: table => new
          {
            Id = table.Column<string>(type: "text", nullable: false),
            FullName = table.Column<string>(type: "text", nullable: true),
            UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
            NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
            Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
            NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
            EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
            PasswordHash = table.Column<string>(type: "text", nullable: true),
            SecurityStamp = table.Column<string>(type: "text", nullable: true),
            ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
            PhoneNumber = table.Column<string>(type: "text", nullable: true),
            PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
            TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
            LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
            LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
            AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_AspNetUsers", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Customers",
          columns: table => new
          {
            Id = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
            Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
            Phone = table.Column<string>(type: "character(10)", fixedLength: true, maxLength: 10, nullable: false),
            Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
            Gender = table.Column<int>(type: "integer", nullable: true),
            ZipCode = table.Column<string>(type: "character(50)", fixedLength: true, maxLength: 50, nullable: false),
            Address = table.Column<Address>(type: "jsonb", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Customers", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Hubs",
          columns: table => new
          {
            Id = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
            ZipCode = table.Column<string>(type: "character(50)", fixedLength: true, maxLength: 50, nullable: false),
            Location = table.Column<Location>(type: "jsonb", nullable: true),
            Status = table.Column<int>(type: "integer", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Hubs", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Staffs",
          columns: table => new
          {
            Id = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
            Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
            Phone = table.Column<string>(type: "character(10)", fixedLength: true, maxLength: 10, nullable: false),
            Position = table.Column<int>(type: "integer", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Staffs", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "AspNetRoleClaims",
          columns: table => new
          {
            Id = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            RoleId = table.Column<string>(type: "text", nullable: false),
            ClaimType = table.Column<string>(type: "text", nullable: true),
            ClaimValue = table.Column<string>(type: "text", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
            table.ForeignKey(
                      name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                      column: x => x.RoleId,
                      principalTable: "AspNetRoles",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "AspNetUserClaims",
          columns: table => new
          {
            Id = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            UserId = table.Column<string>(type: "text", nullable: false),
            ClaimType = table.Column<string>(type: "text", nullable: true),
            ClaimValue = table.Column<string>(type: "text", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
            table.ForeignKey(
                      name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                      column: x => x.UserId,
                      principalTable: "AspNetUsers",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "AspNetUserLogins",
          columns: table => new
          {
            LoginProvider = table.Column<string>(type: "text", nullable: false),
            ProviderKey = table.Column<string>(type: "text", nullable: false),
            ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
            UserId = table.Column<string>(type: "text", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
            table.ForeignKey(
                      name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                      column: x => x.UserId,
                      principalTable: "AspNetUsers",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "AspNetUserRoles",
          columns: table => new
          {
            UserId = table.Column<string>(type: "text", nullable: false),
            RoleId = table.Column<string>(type: "text", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
            table.ForeignKey(
                      name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                      column: x => x.RoleId,
                      principalTable: "AspNetRoles",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                      column: x => x.UserId,
                      principalTable: "AspNetUsers",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "AspNetUserTokens",
          columns: table => new
          {
            UserId = table.Column<string>(type: "text", nullable: false),
            LoginProvider = table.Column<string>(type: "text", nullable: false),
            Name = table.Column<string>(type: "text", nullable: false),
            Value = table.Column<string>(type: "text", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
            table.ForeignKey(
                      name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                      column: x => x.UserId,
                      principalTable: "AspNetUsers",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Routes",
          columns: table => new
          {
            Id = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
            Distance = table.Column<double>(type: "double precision", nullable: true),
            StartHubId = table.Column<string>(type: "character varying(26)", nullable: true),
            EndHubId = table.Column<string>(type: "character varying(26)", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Routes", x => x.Id);
            table.ForeignKey(
                      name: "FK_Routes_Hubs_EndHubId",
                      column: x => x.EndHubId,
                      principalTable: "Hubs",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_Routes_Hubs_StartHubId",
                      column: x => x.StartHubId,
                      principalTable: "Hubs",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Vehicles",
          columns: table => new
          {
            Id = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
            ZipCodeCurrent = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
            LicensePlate = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
            VehicleType = table.Column<int>(type: "integer", nullable: false),
            StaffId = table.Column<string>(type: "character varying(26)", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Vehicles", x => x.Id);
            table.ForeignKey(
                      name: "FK_Vehicles_Staffs_StaffId",
                      column: x => x.StaffId,
                      principalTable: "Staffs",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull);
          });

      migrationBuilder.CreateTable(
          name: "Orders",
          columns: table => new
          {
            Id = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
            StartedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            ExpectedDeliveryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            Status = table.Column<int>(type: "integer", nullable: false),
            DestinationZipCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
            Distance = table.Column<double>(type: "double precision", nullable: true),
            VehicleId = table.Column<string>(type: "character varying(26)", nullable: true),
            CustomerId = table.Column<string>(type: "character varying(26)", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Orders", x => x.Id);
            table.ForeignKey(
                      name: "FK_Orders_Customers_CustomerId",
                      column: x => x.CustomerId,
                      principalTable: "Customers",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull);
            table.ForeignKey(
                      name: "FK_Orders_Vehicles_VehicleId",
                      column: x => x.VehicleId,
                      principalTable: "Vehicles",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull);
          });

      migrationBuilder.CreateTable(
          name: "DeliveryProgresses",
          columns: table => new
          {
            Id = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
            CurrentLocation = table.Column<Location>(type: "jsonb", nullable: true),
            PercentComplete = table.Column<byte>(type: "smallint", nullable: false),
            EstimatedTimeRemaining = table.Column<string>(type: "text", nullable: true),
            LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            OrderId = table.Column<string>(type: "character varying(26)", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_DeliveryProgresses", x => x.Id);
            table.ForeignKey(
                      name: "FK_DeliveryProgresses_Orders_OrderId",
                      column: x => x.OrderId,
                      principalTable: "Orders",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "OrderHistories",
          columns: table => new
          {
            Id = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
            Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            OrderId = table.Column<string>(type: "character varying(26)", nullable: true),
            VehicleId = table.Column<string>(type: "character varying(26)", nullable: true),
            HubId = table.Column<string>(type: "character varying(26)", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_OrderHistories", x => x.Id);
            table.ForeignKey(
                      name: "FK_OrderHistories_Hubs_HubId",
                      column: x => x.HubId,
                      principalTable: "Hubs",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull);
            table.ForeignKey(
                      name: "FK_OrderHistories_Orders_OrderId",
                      column: x => x.OrderId,
                      principalTable: "Orders",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_OrderHistories_Vehicles_VehicleId",
                      column: x => x.VehicleId,
                      principalTable: "Vehicles",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull);
          });

      migrationBuilder.CreateTable(
          name: "Incidents",
          columns: table => new
          {
            Id = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
            Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
            Status = table.Column<int>(type: "integer", nullable: false),
            Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            OrderHistoryId = table.Column<string>(type: "character varying(26)", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Incidents", x => x.Id);
            table.ForeignKey(
                      name: "FK_Incidents_OrderHistories_OrderHistoryId",
                      column: x => x.OrderHistoryId,
                      principalTable: "OrderHistories",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_AspNetRoleClaims_RoleId",
          table: "AspNetRoleClaims",
          column: "RoleId");

      migrationBuilder.CreateIndex(
          name: "RoleNameIndex",
          table: "AspNetRoles",
          column: "NormalizedName",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_AspNetUserClaims_UserId",
          table: "AspNetUserClaims",
          column: "UserId");

      migrationBuilder.CreateIndex(
          name: "IX_AspNetUserLogins_UserId",
          table: "AspNetUserLogins",
          column: "UserId");

      migrationBuilder.CreateIndex(
          name: "IX_AspNetUserRoles_RoleId",
          table: "AspNetUserRoles",
          column: "RoleId");

      migrationBuilder.CreateIndex(
          name: "EmailIndex",
          table: "AspNetUsers",
          column: "NormalizedEmail");

      migrationBuilder.CreateIndex(
          name: "UserNameIndex",
          table: "AspNetUsers",
          column: "NormalizedUserName",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_DeliveryProgresses_OrderId",
          table: "DeliveryProgresses",
          column: "OrderId");

      migrationBuilder.CreateIndex(
          name: "IX_Incidents_OrderHistoryId",
          table: "Incidents",
          column: "OrderHistoryId");

      migrationBuilder.CreateIndex(
          name: "IX_OrderHistories_HubId",
          table: "OrderHistories",
          column: "HubId");

      migrationBuilder.CreateIndex(
          name: "IX_OrderHistories_OrderId",
          table: "OrderHistories",
          column: "OrderId");

      migrationBuilder.CreateIndex(
          name: "IX_OrderHistories_VehicleId",
          table: "OrderHistories",
          column: "VehicleId");

      migrationBuilder.CreateIndex(
          name: "IX_Orders_CustomerId",
          table: "Orders",
          column: "CustomerId");

      migrationBuilder.CreateIndex(
          name: "IX_Orders_VehicleId",
          table: "Orders",
          column: "VehicleId");

      migrationBuilder.CreateIndex(
          name: "IX_Routes_EndHubId",
          table: "Routes",
          column: "EndHubId");

      migrationBuilder.CreateIndex(
          name: "IX_Routes_StartHubId",
          table: "Routes",
          column: "StartHubId");

      migrationBuilder.CreateIndex(
          name: "IX_Vehicles_StaffId",
          table: "Vehicles",
          column: "StaffId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "AspNetRoleClaims");

      migrationBuilder.DropTable(
          name: "AspNetUserClaims");

      migrationBuilder.DropTable(
          name: "AspNetUserLogins");

      migrationBuilder.DropTable(
          name: "AspNetUserRoles");

      migrationBuilder.DropTable(
          name: "AspNetUserTokens");

      migrationBuilder.DropTable(
          name: "DeliveryProgresses");

      migrationBuilder.DropTable(
          name: "Incidents");

      migrationBuilder.DropTable(
          name: "Routes");

      migrationBuilder.DropTable(
          name: "AspNetRoles");

      migrationBuilder.DropTable(
          name: "AspNetUsers");

      migrationBuilder.DropTable(
          name: "OrderHistories");

      migrationBuilder.DropTable(
          name: "Hubs");

      migrationBuilder.DropTable(
          name: "Orders");

      migrationBuilder.DropTable(
          name: "Customers");

      migrationBuilder.DropTable(
          name: "Vehicles");

      migrationBuilder.DropTable(
          name: "Staffs");
    }
  }
}
