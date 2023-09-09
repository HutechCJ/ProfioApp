using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Profio.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Impotency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_Orders_OrderId",
                table: "Delivery");

            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_Vehicles_VehicleId",
                table: "Delivery");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistories_Delivery_DeliveryId",
                table: "OrderHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Delivery",
                table: "Delivery");

            migrationBuilder.RenameTable(
                name: "Delivery",
                newName: "Deliveries");

            migrationBuilder.RenameIndex(
                name: "IX_Delivery_VehicleId",
                table: "Deliveries",
                newName: "IX_Deliveries_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Delivery_OrderId",
                table: "Deliveries",
                newName: "IX_Deliveries_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deliveries",
                table: "Deliveries",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "IdempotentRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdempotentRequests", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Orders_OrderId",
                table: "Deliveries",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Vehicles_VehicleId",
                table: "Deliveries",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistories_Deliveries_DeliveryId",
                table: "OrderHistories",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Orders_OrderId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Vehicles_VehicleId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistories_Deliveries_DeliveryId",
                table: "OrderHistories");

            migrationBuilder.DropTable(
                name: "IdempotentRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deliveries",
                table: "Deliveries");

            migrationBuilder.RenameTable(
                name: "Deliveries",
                newName: "Delivery");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveries_VehicleId",
                table: "Delivery",
                newName: "IX_Delivery_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveries_OrderId",
                table: "Delivery",
                newName: "IX_Delivery_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Delivery",
                table: "Delivery",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_Orders_OrderId",
                table: "Delivery",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_Vehicles_VehicleId",
                table: "Delivery",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistories_Delivery_DeliveryId",
                table: "OrderHistories",
                column: "DeliveryId",
                principalTable: "Delivery",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
