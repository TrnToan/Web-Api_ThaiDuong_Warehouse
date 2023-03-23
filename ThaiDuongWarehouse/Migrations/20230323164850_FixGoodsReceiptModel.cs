using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThaiDuongWarehouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixGoodsReceiptModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Supplier",
                table: "GoodsReceipts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Supplier",
                table: "GoodsReceipts");
        }
    }
}
