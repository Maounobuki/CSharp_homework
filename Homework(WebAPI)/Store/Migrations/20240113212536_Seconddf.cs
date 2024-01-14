using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetStore.Migrations
{
    /// <inheritdoc />
    public partial class Seconddf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductGroups_Id",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProductGroups",
                newName: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "GroupToProduct",
                table: "Products",
                column: "Id",
                principalTable: "ProductGroups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "GroupToProduct",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "ProductGroups",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductGroups_Id",
                table: "Products",
                column: "Id",
                principalTable: "ProductGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
