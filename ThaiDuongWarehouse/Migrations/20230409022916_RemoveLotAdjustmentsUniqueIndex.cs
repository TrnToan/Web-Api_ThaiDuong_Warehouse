using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThaiDuongWarehouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLotAdjustmentsUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LotAdjustments_LotId",
                table: "LotAdjustments");

            migrationBuilder.CreateIndex(
                name: "IX_LotAdjustments_LotId",
                table: "LotAdjustments",
                column: "LotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LotAdjustments_LotId",
                table: "LotAdjustments");

            migrationBuilder.CreateIndex(
                name: "IX_LotAdjustments_LotId",
                table: "LotAdjustments",
                column: "LotId",
                unique: true);
        }
    }
}
