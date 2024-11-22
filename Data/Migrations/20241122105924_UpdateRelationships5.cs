using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace freak_store.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationships5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Discounts_DiscountId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Discounts_DiscountId1",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Inventory_InventoryId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_DiscountId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_InventoryId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DiscountId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "InventoryId1",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Discounts_DiscountId",
                table: "Products",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Discounts_DiscountId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "DiscountId1",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InventoryId1",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_DiscountId1",
                table: "Products",
                column: "DiscountId1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_InventoryId1",
                table: "Products",
                column: "InventoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Discounts_DiscountId",
                table: "Products",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Discounts_DiscountId1",
                table: "Products",
                column: "DiscountId1",
                principalTable: "Discounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Inventory_InventoryId1",
                table: "Products",
                column: "InventoryId1",
                principalTable: "Inventory",
                principalColumn: "Id");
        }
    }
}
