using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThaiDuongWarehouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSublotEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GoodsReceiptSublot",
                table: "GoodsReceiptSublot");

            migrationBuilder.DropIndex(
                name: "IX_GoodsReceiptSublot_GoodsReceiptLotId",
                table: "GoodsReceiptSublot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GoodsIssueSublot",
                table: "GoodsIssueSublot");

            migrationBuilder.DropIndex(
                name: "IX_GoodsIssueSublot_GoodsIssueLotGoodsIssueEntryId_GoodsIssueLotId",
                table: "GoodsIssueSublot");

            migrationBuilder.AlterColumn<string>(
                name: "LocationId",
                table: "GoodsReceiptSublot",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "GoodsReceiptSublot",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "LocationId",
                table: "GoodsIssueSublot",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "GoodsIssueSublot",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoodsReceiptSublot",
                table: "GoodsReceiptSublot",
                columns: new[] { "GoodsReceiptLotId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoodsIssueSublot",
                table: "GoodsIssueSublot",
                columns: new[] { "GoodsIssueLotGoodsIssueEntryId", "GoodsIssueLotId", "Id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GoodsReceiptSublot",
                table: "GoodsReceiptSublot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GoodsIssueSublot",
                table: "GoodsIssueSublot");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GoodsReceiptSublot");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GoodsIssueSublot");

            migrationBuilder.AlterColumn<string>(
                name: "LocationId",
                table: "GoodsReceiptSublot",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LocationId",
                table: "GoodsIssueSublot",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoodsReceiptSublot",
                table: "GoodsReceiptSublot",
                column: "LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoodsIssueSublot",
                table: "GoodsIssueSublot",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptSublot_GoodsReceiptLotId",
                table: "GoodsReceiptSublot",
                column: "GoodsReceiptLotId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssueSublot_GoodsIssueLotGoodsIssueEntryId_GoodsIssueLotId",
                table: "GoodsIssueSublot",
                columns: new[] { "GoodsIssueLotGoodsIssueEntryId", "GoodsIssueLotId" });
        }
    }
}
