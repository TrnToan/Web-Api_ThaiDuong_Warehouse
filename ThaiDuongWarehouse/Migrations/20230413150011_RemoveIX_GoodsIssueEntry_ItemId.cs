using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThaiDuongWarehouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIX_GoodsIssueEntry_ItemId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GoodsIssueEntry_ItemId",
                table: "GoodsIssueEntry");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssueEntry_ItemId",
                table: "GoodsIssueEntry",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GoodsIssueEntry_ItemId",
                table: "GoodsIssueEntry");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssueEntry_ItemId",
                table: "GoodsIssueEntry",
                column: "ItemId",
                unique: true);
        }
    }
}
