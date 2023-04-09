using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThaiDuongWarehouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLotAdjustmentUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LotAdjustments_ItemId",
                table: "LotAdjustments");

            migrationBuilder.CreateIndex(
                name: "IX_LotAdjustments_ItemId",
                table: "LotAdjustments",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LotAdjustments_ItemId",
                table: "LotAdjustments");

            migrationBuilder.CreateIndex(
                name: "IX_LotAdjustments_ItemId",
                table: "LotAdjustments",
                column: "ItemId",
                unique: true);
        }
    }
}
