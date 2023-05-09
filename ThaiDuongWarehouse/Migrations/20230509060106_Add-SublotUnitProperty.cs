using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThaiDuongWarehouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSublotUnitProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SublotUnit",
                table: "ItemLots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SublotUnit",
                table: "GoodsReceiptLot",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SublotUnit",
                table: "GoodsIssueLot",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SublotUnit",
                table: "ItemLots");

            migrationBuilder.DropColumn(
                name: "SublotUnit",
                table: "GoodsReceiptLot");

            migrationBuilder.DropColumn(
                name: "SublotUnit",
                table: "GoodsIssueLot");
        }
    }
}
