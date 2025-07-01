using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class DataModelForShoppingCartUpdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_shoppingCarts_ShoppingCartCartId",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_ShoppingCartCartId",
                table: "product");

            migrationBuilder.DropColumn(
                name: "ShoppingCartCartId",
                table: "product");

            migrationBuilder.AddColumn<string>(
                name: "ProductList",
                table: "shoppingCarts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductList",
                table: "shoppingCarts");

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
    }
}
