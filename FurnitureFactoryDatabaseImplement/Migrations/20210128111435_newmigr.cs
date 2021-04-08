using Microsoft.EntityFrameworkCore.Migrations;

namespace FurnitureFactoryDatabaseImplement.Migrations
{
    public partial class newmigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FurnitureTypeId",
                table: "FurnitureModels");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Payments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalSum",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "FurnitureModels",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_FurnitureModelId",
                table: "Positions",
                column: "FurnitureModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_OrderId",
                table: "Positions",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ClientId",
                table: "Payments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Clients_ClientId",
                table: "Orders",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Clients_ClientId",
                table: "Payments",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Orders_OrderId",
                table: "Payments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_FurnitureModels_FurnitureModelId",
                table: "Positions",
                column: "FurnitureModelId",
                principalTable: "FurnitureModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Orders_OrderId",
                table: "Positions",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Clients_ClientId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Clients_ClientId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Orders_OrderId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_FurnitureModels_FurnitureModelId",
                table: "Positions");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Orders_OrderId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_FurnitureModelId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_OrderId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ClientId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_OrderId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ClientId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "TotalSum",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "FurnitureModels");

            migrationBuilder.AddColumn<int>(
                name: "FurnitureTypeId",
                table: "FurnitureModels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
