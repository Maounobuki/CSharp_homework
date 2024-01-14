using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetStore.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorageProduct_Products_ProductsId",
                table: "StorageProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageProduct_Storage_StoresId",
                table: "StorageProduct");

            migrationBuilder.RenameColumn(
                name: "StoresId",
                table: "StorageProduct",
                newName: "StoresWhId");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "StorageProduct",
                newName: "ProductsProdId");

            migrationBuilder.RenameIndex(
                name: "IX_StorageProduct_StoresId",
                table: "StorageProduct",
                newName: "IX_StorageProduct_StoresWhId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Storage",
                newName: "WhId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ProdId");

            migrationBuilder.AddForeignKey(
                name: "FK_StorageProduct_Products_ProductsProdId",
                table: "StorageProduct",
                column: "ProductsProdId",
                principalTable: "Products",
                principalColumn: "ProdId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageProduct_Storage_StoresWhId",
                table: "StorageProduct",
                column: "StoresWhId",
                principalTable: "Storage",
                principalColumn: "WhId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorageProduct_Products_ProductsProdId",
                table: "StorageProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageProduct_Storage_StoresWhId",
                table: "StorageProduct");

            migrationBuilder.RenameColumn(
                name: "StoresWhId",
                table: "StorageProduct",
                newName: "StoresId");

            migrationBuilder.RenameColumn(
                name: "ProductsProdId",
                table: "StorageProduct",
                newName: "ProductsId");

            migrationBuilder.RenameIndex(
                name: "IX_StorageProduct_StoresWhId",
                table: "StorageProduct",
                newName: "IX_StorageProduct_StoresId");

            migrationBuilder.RenameColumn(
                name: "WhId",
                table: "Storage",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProdId",
                table: "Products",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StorageProduct_Products_ProductsId",
                table: "StorageProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageProduct_Storage_StoresId",
                table: "StorageProduct",
                column: "StoresId",
                principalTable: "Storage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
