using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Profio.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_OrderHistories_OrderHistoryId",
                table: "Incidents");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistories_Deliveries_DeliveryId",
                table: "OrderHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistories_Hubs_HubId",
                table: "OrderHistories");

            migrationBuilder.RenameColumn(
                name: "OrderHistoryId",
                table: "Incidents",
                newName: "DeliveryId");

            migrationBuilder.RenameIndex(
                name: "IX_Incidents_OrderHistoryId",
                table: "Incidents",
                newName: "IX_Incidents_DeliveryId");

            migrationBuilder.AddColumn<string>(
                name: "PhaseId",
                table: "Orders",
                type: "character varying(26)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "OrderHistories",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(26)",
                oldMaxLength: 26);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StaffId",
                table: "AspNetUsers",
                type: "character varying(26)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Phase",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(26)", maxLength: 26, nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    RouteId = table.Column<string>(type: "character varying(26)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phase_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PhaseId",
                table: "Orders",
                column: "PhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StaffId",
                table: "AspNetUsers",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Phase_RouteId",
                table: "Phase",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Staffs_StaffId",
                table: "AspNetUsers",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Deliveries_DeliveryId",
                table: "Incidents",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistories_Deliveries_DeliveryId",
                table: "OrderHistories",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistories_Hubs_HubId",
                table: "OrderHistories",
                column: "HubId",
                principalTable: "Hubs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Phase_PhaseId",
                table: "Orders",
                column: "PhaseId",
                principalTable: "Phase",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Staffs_StaffId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Deliveries_DeliveryId",
                table: "Incidents");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistories_Deliveries_DeliveryId",
                table: "OrderHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistories_Hubs_HubId",
                table: "OrderHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Phase_PhaseId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Phase");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PhaseId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StaffId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhaseId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "DeliveryId",
                table: "Incidents",
                newName: "OrderHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Incidents_DeliveryId",
                table: "Incidents",
                newName: "IX_Incidents_OrderHistoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "OrderHistories",
                type: "character varying(26)",
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_OrderHistories_OrderHistoryId",
                table: "Incidents",
                column: "OrderHistoryId",
                principalTable: "OrderHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistories_Deliveries_DeliveryId",
                table: "OrderHistories",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistories_Hubs_HubId",
                table: "OrderHistories",
                column: "HubId",
                principalTable: "Hubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
