using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class DataModelForShoppingCartUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shoppingCarts_product_ProductId",
                table: "shoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_shoppingCarts_ProductId",
                table: "shoppingCarts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "shoppingCarts");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "shoppingCarts",
                newName: "TotalQuantity");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "shoppingCarts",
                newName: "TotalPrice");

            migrationBuilder.AddColumn<bool>(
                name: "isCheckedOut",
                table: "shoppingCarts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartCartId",
                table: "product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_ShoppingCartCartId",
                table: "product",
                column: "ShoppingCartCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_product_shoppingCarts_ShoppingCartCartId",
                table: "product",
                column: "ShoppingCartCartId",
                principalTable: "shoppingCarts",
                principalColumn: "CartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_shoppingCarts_ShoppingCartCartId",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_ShoppingCartCartId",
                table: "product");

            migrationBuilder.DropColumn(
                name: "isCheckedOut",
                table: "shoppingCarts");

            migrationBuilder.DropColumn(
                name: "ShoppingCartCartId",
                table: "product");

            migrationBuilder.RenameColumn(
                name: "TotalQuantity",
                table: "shoppingCarts",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "shoppingCarts",
                newName: "Price");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "shoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_shoppingCarts_ProductId",
                table: "shoppingCarts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_shoppingCarts_product_ProductId",
                table: "shoppingCarts",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
