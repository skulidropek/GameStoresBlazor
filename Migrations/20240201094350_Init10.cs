using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStoresBlazor.Migrations
{
    /// <inheritdoc />
    public partial class Init10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserInventoryProductId",
                table: "Transactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserInventoryProductId",
                table: "Transactions",
                column: "UserInventoryProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Inventories_UserInventoryProductId",
                table: "Transactions",
                column: "UserInventoryProductId",
                principalTable: "Inventories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Inventories_UserInventoryProductId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_UserInventoryProductId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UserInventoryProductId",
                table: "Transactions");
        }
    }
}
