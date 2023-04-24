using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThaiDuongWarehouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class NewPKGoodsIssueLot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GoodsIssueLot",
                table: "GoodsIssueLot");

            migrationBuilder.DropIndex(
                name: "IX_GoodsIssueLot_GoodsIssueEntryId",
                table: "GoodsIssueLot");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoodsIssueLot",
                table: "GoodsIssueLot",
                columns: new[] { "GoodsIssueEntryId", "GoodsIssueLotId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GoodsIssueLot",
                table: "GoodsIssueLot");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoodsIssueLot",
                table: "GoodsIssueLot",
                column: "GoodsIssueLotId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssueLot_GoodsIssueEntryId",
                table: "GoodsIssueLot",
                column: "GoodsIssueEntryId");
        }
    }
}
