using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace freak_store.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameProductColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_discounts_DiscountId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_inventory_InventoryId",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "InventoryId",
                table: "products",
                newName: "inventory_id");

            migrationBuilder.RenameColumn(
                name: "DiscountId",
                table: "products",
                newName: "discount_id");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "products",
                newName: "category_id");

            migrationBuilder.RenameIndex(
                name: "IX_products_InventoryId",
                table: "products",
                newName: "IX_products_inventory_id");

            migrationBuilder.RenameIndex(
                name: "IX_products_DiscountId",
                table: "products",
                newName: "IX_products_discount_id");

            migrationBuilder.RenameIndex(
                name: "IX_products_CategoryId",
                table: "products",
                newName: "IX_products_category_id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_category_id",
                table: "products",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_discounts_discount_id",
                table: "products",
                column: "discount_id",
                principalTable: "discounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_inventory_inventory_id",
                table: "products",
                column: "inventory_id",
                principalTable: "inventory",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_category_id",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_discounts_discount_id",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_inventory_inventory_id",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "inventory_id",
                table: "products",
                newName: "InventoryId");

            migrationBuilder.RenameColumn(
                name: "discount_id",
                table: "products",
                newName: "DiscountId");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "products",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_products_inventory_id",
                table: "products",
                newName: "IX_products_InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_products_discount_id",
                table: "products",
                newName: "IX_products_DiscountId");

            migrationBuilder.RenameIndex(
                name: "IX_products_category_id",
                table: "products",
                newName: "IX_products_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_discounts_DiscountId",
                table: "products",
                column: "DiscountId",
                principalTable: "discounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_inventory_InventoryId",
                table: "products",
                column: "InventoryId",
                principalTable: "inventory",
                principalColumn: "id");
        }
    }
}
