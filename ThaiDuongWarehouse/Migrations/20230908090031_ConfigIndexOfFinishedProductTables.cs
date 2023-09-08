using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThaiDuongWarehouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class ConfigIndexOfFinishedProductTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FinishedProductReceiptEntry_ItemId_PurchaseOrderNumber",
                table: "FinishedProductReceiptEntry");

            migrationBuilder.DropIndex(
                name: "IX_FinishedProductIssueEntry_ItemId_PurchaseOrderNumber",
                table: "FinishedProductIssueEntry");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProductReceiptEntry_ItemId_PurchaseOrderNumber_FinishedProductReceiptId",
                table: "FinishedProductReceiptEntry",
                columns: new[] { "ItemId", "PurchaseOrderNumber", "FinishedProductReceiptId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProductIssueEntry_ItemId_PurchaseOrderNumber_FinishedProductIssueId",
                table: "FinishedProductIssueEntry",
                columns: new[] { "ItemId", "PurchaseOrderNumber", "FinishedProductIssueId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FinishedProductReceiptEntry_ItemId_PurchaseOrderNumber_FinishedProductReceiptId",
                table: "FinishedProductReceiptEntry");

            migrationBuilder.DropIndex(
                name: "IX_FinishedProductIssueEntry_ItemId_PurchaseOrderNumber_FinishedProductIssueId",
                table: "FinishedProductIssueEntry");

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
    }
}
