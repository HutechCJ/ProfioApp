using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Profio.Domain.ValueObjects;

#nullable disable

namespace Profio.Infrastructure.Persistence.Migrations
{
  /// <inheritdoc />
  public partial class EditEntities : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_OrderHistories_Orders_OrderId",
          table: "OrderHistories");

      migrationBuilder.DropForeignKey(
          name: "FK_OrderHistories_Vehicles_VehicleId",
          table: "OrderHistories");

      migrationBuilder.DropForeignKey(
          name: "FK_Orders_Vehicles_VehicleId",
          table: "Orders");

      migrationBuilder.DropIndex(
          name: "IX_Orders_VehicleId",
          table: "Orders");

      migrationBuilder.DropIndex(
          name: "IX_OrderHistories_OrderId",
          table: "OrderHistories");

      migrationBuilder.DropColumn(
          name: "VehicleId",
          table: "Orders");

      migrationBuilder.DropColumn(
          name: "OrderId",
          table: "OrderHistories");

      migrationBuilder.DropColumn(
          name: "ZipCode",
          table: "Customers");

      migrationBuilder.RenameColumn(
          name: "VehicleType",
          table: "Vehicles",
          newName: "Type");

      migrationBuilder.RenameColumn(
          name: "VehicleId",
          table: "OrderHistories",
          newName: "DeliveryId");

      migrationBuilder.RenameIndex(
          name: "IX_OrderHistories_VehicleId",
          table: "OrderHistories",
          newName: "IX_OrderHistories_DeliveryId");

      migrationBuilder.AddColumn<int>(
          name: "Status",
          table: "Vehicles",
          type: "integer",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<Address>(
          name: "DestinationAddress",
          table: "Orders",
          type: "jsonb",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "Note",
          table: "Orders",
          type: "character varying(250)",
          maxLength: 250,
          nullable: true);

      migrationBuilder.AddColumn<Address>(
          name: "Address",
          table: "Hubs",
          type: "jsonb",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "Name",
          table: "Hubs",
          type: "character varying(50)",
          maxLength: 50,
          nullable: false,
          defaultValue: "");

      migrationBuilder.CreateTable(
          name: "Delivery",
          columns: table => new
          {
            Id = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
            DeliveryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            OrderId = table.Column<string>(type: "character varying(26)", nullable: true),
            VehicleId = table.Column<string>(type: "character varying(26)", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Delivery", x => x.Id);
            table.ForeignKey(
                      name: "FK_Delivery_Orders_OrderId",
                      column: x => x.OrderId,
                      principalTable: "Orders",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_Delivery_Vehicles_VehicleId",
                      column: x => x.VehicleId,
                      principalTable: "Vehicles",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Delivery_OrderId",
          table: "Delivery",
          column: "OrderId");

      migrationBuilder.CreateIndex(
          name: "IX_Delivery_VehicleId",
          table: "Delivery",
          column: "VehicleId");

      migrationBuilder.AddForeignKey(
          name: "FK_OrderHistories_Delivery_DeliveryId",
          table: "OrderHistories",
          column: "DeliveryId",
          principalTable: "Delivery",
          principalColumn: "Id",
          onDelete: ReferentialAction.SetNull);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_OrderHistories_Delivery_DeliveryId",
          table: "OrderHistories");

      migrationBuilder.DropTable(
          name: "Delivery");

      migrationBuilder.DropColumn(
          name: "Status",
          table: "Vehicles");

      migrationBuilder.DropColumn(
          name: "DestinationAddress",
          table: "Orders");

      migrationBuilder.DropColumn(
          name: "Note",
          table: "Orders");

      migrationBuilder.DropColumn(
          name: "Address",
          table: "Hubs");

      migrationBuilder.DropColumn(
          name: "Name",
          table: "Hubs");

      migrationBuilder.RenameColumn(
          name: "Type",
          table: "Vehicles",
          newName: "VehicleType");

      migrationBuilder.RenameColumn(
          name: "DeliveryId",
          table: "OrderHistories",
          newName: "VehicleId");

      migrationBuilder.RenameIndex(
          name: "IX_OrderHistories_DeliveryId",
          table: "OrderHistories",
          newName: "IX_OrderHistories_VehicleId");

      migrationBuilder.AddColumn<string>(
          name: "VehicleId",
          table: "Orders",
          type: "character varying(26)",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "OrderId",
          table: "OrderHistories",
          type: "character varying(26)",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "ZipCode",
          table: "Customers",
          type: "character(50)",
          fixedLength: true,
          maxLength: 50,
          nullable: false,
          defaultValue: "");

      migrationBuilder.CreateIndex(
          name: "IX_Orders_VehicleId",
          table: "Orders",
          column: "VehicleId");

      migrationBuilder.CreateIndex(
          name: "IX_OrderHistories_OrderId",
          table: "OrderHistories",
          column: "OrderId");

      migrationBuilder.AddForeignKey(
          name: "FK_OrderHistories_Orders_OrderId",
          table: "OrderHistories",
          column: "OrderId",
          principalTable: "Orders",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "FK_OrderHistories_Vehicles_VehicleId",
          table: "OrderHistories",
          column: "VehicleId",
          principalTable: "Vehicles",
          principalColumn: "Id",
          onDelete: ReferentialAction.SetNull);

      migrationBuilder.AddForeignKey(
          name: "FK_Orders_Vehicles_VehicleId",
          table: "Orders",
          column: "VehicleId",
          principalTable: "Vehicles",
          principalColumn: "Id",
          onDelete: ReferentialAction.SetNull);
    }
  }
}
