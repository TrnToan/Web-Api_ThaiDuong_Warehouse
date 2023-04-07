using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThaiDuongWarehouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class GenerateGoodsReceiptFKMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "GoodsReceiptLot",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "GoodsReceiptLot");
        }
    }
}
