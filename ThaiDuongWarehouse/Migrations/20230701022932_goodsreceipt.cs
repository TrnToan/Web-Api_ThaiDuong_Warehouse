using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThaiDuongWarehouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class goodsreceipt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GoodsReceiptLot",
                table: "GoodsReceiptLot");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "GoodsReceiptLot",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoodsReceiptLot",
                table: "GoodsReceiptLot",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptLot_GoodsReceiptLotId",
                table: "GoodsReceiptLot",
                column: "GoodsReceiptLotId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GoodsReceiptLot",
                table: "GoodsReceiptLot");

            migrationBuilder.DropIndex(
                name: "IX_GoodsReceiptLot_GoodsReceiptLotId",
                table: "GoodsReceiptLot");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GoodsReceiptLot");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoodsReceiptLot",
                table: "GoodsReceiptLot",
                column: "GoodsReceiptLotId");
        }
    }
}
