using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThaiDuongWarehouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveItemIdIndexInGoodsReceiptLotTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GoodsReceiptLot_ItemId",
                table: "GoodsReceiptLot");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptLot_ItemId",
                table: "GoodsReceiptLot",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GoodsReceiptLot_ItemId",
                table: "GoodsReceiptLot");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptLot_ItemId",
                table: "GoodsReceiptLot",
                column: "ItemId",
                unique: true);
        }
    }
}
