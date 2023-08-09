using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThaiDuongWarehouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class SetFinishedProductIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FinishedProductReceiptEntry_ItemId",
                table: "FinishedProductReceiptEntry");

            migrationBuilder.DropIndex(
                name: "IX_FinishedProductIssueEntry_ItemId",
                table: "FinishedProductIssueEntry");

            migrationBuilder.AlterColumn<string>(
                name: "PurchaseOrderNumber",
                table: "FinishedProductReceiptEntry",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PurchaseOrderNumber",
                table: "FinishedProductIssueEntry",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProductReceiptEntry_ItemId_PurchaseOrderNumber",
                table: "FinishedProductReceiptEntry",
                columns: new[] { "ItemId", "PurchaseOrderNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProductIssueEntry_ItemId_PurchaseOrderNumber",
                table: "FinishedProductIssueEntry",
                columns: new[] { "ItemId", "PurchaseOrderNumber" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FinishedProductReceiptEntry_ItemId_PurchaseOrderNumber",
                table: "FinishedProductReceiptEntry");

            migrationBuilder.DropIndex(
                name: "IX_FinishedProductIssueEntry_ItemId_PurchaseOrderNumber",
                table: "FinishedProductIssueEntry");

            migrationBuilder.AlterColumn<string>(
                name: "PurchaseOrderNumber",
                table: "FinishedProductReceiptEntry",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PurchaseOrderNumber",
                table: "FinishedProductIssueEntry",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProductReceiptEntry_ItemId",
                table: "FinishedProductReceiptEntry",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProductIssueEntry_ItemId",
                table: "FinishedProductIssueEntry",
                column: "ItemId");
        }
    }
}
