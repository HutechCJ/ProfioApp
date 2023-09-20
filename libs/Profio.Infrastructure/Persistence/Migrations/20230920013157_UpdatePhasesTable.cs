using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Profio.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePhasesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Phase_PhaseId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Phase_Routes_RouteId",
                table: "Phase");

            migrationBuilder.DropTable(
                name: "OrderHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Phase",
                table: "Phase");

            migrationBuilder.RenameTable(
                name: "Phase",
                newName: "Phases");

            migrationBuilder.RenameIndex(
                name: "IX_Phase_RouteId",
                table: "Phases",
                newName: "IX_Phases_RouteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Phases",
                table: "Phases",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Phases_PhaseId",
                table: "Orders",
                column: "PhaseId",
                principalTable: "Phases",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Phases_Routes_RouteId",
                table: "Phases",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Phases_PhaseId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Phases_Routes_RouteId",
                table: "Phases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Phases",
                table: "Phases");

            migrationBuilder.RenameTable(
                name: "Phases",
                newName: "Phase");

            migrationBuilder.RenameIndex(
                name: "IX_Phases_RouteId",
                table: "Phase",
                newName: "IX_Phase_RouteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Phase",
                table: "Phase",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OrderHistories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    DeliveryId = table.Column<string>(type: "character varying(26)", nullable: true),
                    HubId = table.Column<string>(type: "character varying(26)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHistories_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderHistories_Hubs_HubId",
                        column: x => x.HubId,
                        principalTable: "Hubs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_DeliveryId",
                table: "OrderHistories",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_HubId",
                table: "OrderHistories",
                column: "HubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Phase_PhaseId",
                table: "Orders",
                column: "PhaseId",
                principalTable: "Phase",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Phase_Routes_RouteId",
                table: "Phase",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
