using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PicutreUrl",
                table: "Products",
                newName: "PictureUrl");

            migrationBuilder.RenameColumn(
                name: "ItemOrderd_ProductName",
                table: "OrderItem",
                newName: "ProductItem_ProductName");

            migrationBuilder.RenameColumn(
                name: "ItemOrderd_ProductId",
                table: "OrderItem",
                newName: "ProductItem_ProductId");

            migrationBuilder.RenameColumn(
                name: "ItemOrderd_ProductUrl",
                table: "OrderItem",
                newName: "ProductItem_PictureUrl");

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "PictureUrl",
                table: "Products",
                newName: "PicutreUrl");

            migrationBuilder.RenameColumn(
                name: "ProductItem_ProductName",
                table: "OrderItem",
                newName: "ItemOrderd_ProductName");

            migrationBuilder.RenameColumn(
                name: "ProductItem_ProductId",
                table: "OrderItem",
                newName: "ItemOrderd_ProductId");

            migrationBuilder.RenameColumn(
                name: "ProductItem_PictureUrl",
                table: "OrderItem",
                newName: "ItemOrderd_ProductUrl");
        }
    }
}
