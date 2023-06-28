using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThaiDuongWarehouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class RebuildDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewPurchaseOrderNumber",
                table: "LotAdjustments");

            migrationBuilder.DropColumn(
                name: "OldPurchaseOrderNumber",
                table: "LotAdjustments");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "LotAdjustments");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderNumber",
                table: "ItemLots");

            migrationBuilder.DropColumn(
                name: "SublotSize",
                table: "ItemLots");

            migrationBuilder.DropColumn(
                name: "SublotUnit",
                table: "ItemLots");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "ItemLots");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "InventoryLogEntries");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "GoodsReceipts");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderNumber",
                table: "GoodsReceiptLot");

            migrationBuilder.DropColumn(
                name: "SublotSize",
                table: "GoodsReceiptLot");

            migrationBuilder.DropColumn(
                name: "SublotUnit",
                table: "GoodsReceiptLot");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "GoodsReceiptLot");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "GoodsIssues");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderNumber",
                table: "GoodsIssues");

            migrationBuilder.DropColumn(
                name: "SublotSize",
                table: "GoodsIssueLot");

            migrationBuilder.DropColumn(
                name: "SublotUnit",
                table: "GoodsIssueLot");

            migrationBuilder.DropColumn(
                name: "RequestedSublotSize",
                table: "GoodsIssueEntry");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "GoodsIssueEntry");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Items",
                type: "decimal(12,2)",
                precision: 12,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,2)",
                oldPrecision: 12,
                oldScale: 2);

            migrationBuilder.AddColumn<string>(
                name: "PacketUnit",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SublotSize",
                table: "Items",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "ItemLots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "ItemLotId",
                table: "InventoryLogEntries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Receiver",
                table: "GoodsIssues",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "FinisedProductIssues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinishedProductIssueId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Receiver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinisedProductIssues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinisedProductIssues_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinishedProductInventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishedProductInventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinishedProductInventories_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinishedProductReceipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinishedProductReceiptId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishedProductReceipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinishedProductReceipts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinisedProductIssueEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishedProductIssueId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinisedProductIssueEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinisedProductIssueEntry_FinisedProductIssues_FinishedProductIssueId",
                        column: x => x.FinishedProductIssueId,
                        principalTable: "FinisedProductIssues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinisedProductIssueEntry_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinishedProductReceiptEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishedProductReceiptId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishedProductReceiptEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinishedProductReceiptEntry_FinishedProductReceipts_FinishedProductReceiptId",
                        column: x => x.FinishedProductReceiptId,
                        principalTable: "FinishedProductReceipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinishedProductReceiptEntry_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinisedProductIssueEntry_FinishedProductIssueId",
                table: "FinisedProductIssueEntry",
                column: "FinishedProductIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_FinisedProductIssueEntry_ItemId",
                table: "FinisedProductIssueEntry",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FinisedProductIssues_EmployeeId",
                table: "FinisedProductIssues",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinisedProductIssues_FinishedProductIssueId",
                table: "FinisedProductIssues",
                column: "FinishedProductIssueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProductInventories_ItemId",
                table: "FinishedProductInventories",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProductReceiptEntry_FinishedProductReceiptId",
                table: "FinishedProductReceiptEntry",
                column: "FinishedProductReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProductReceiptEntry_ItemId",
                table: "FinishedProductReceiptEntry",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProductReceipts_EmployeeId",
                table: "FinishedProductReceipts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProductReceipts_FinishedProductReceiptId",
                table: "FinishedProductReceipts",
                column: "FinishedProductReceiptId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinisedProductIssueEntry");

            migrationBuilder.DropTable(
                name: "FinishedProductInventories");

            migrationBuilder.DropTable(
                name: "FinishedProductReceiptEntry");

            migrationBuilder.DropTable(
                name: "FinisedProductIssues");

            migrationBuilder.DropTable(
                name: "FinishedProductReceipts");

            migrationBuilder.DropColumn(
                name: "PacketUnit",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SublotSize",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "ItemLots");

            migrationBuilder.AddColumn<string>(
                name: "NewPurchaseOrderNumber",
                table: "LotAdjustments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OldPurchaseOrderNumber",
                table: "LotAdjustments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "LotAdjustments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Items",
                type: "decimal(12,2)",
                precision: 12,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,2)",
                oldPrecision: 12,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchaseOrderNumber",
                table: "ItemLots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SublotSize",
                table: "ItemLots",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SublotUnit",
                table: "ItemLots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "ItemLots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ItemLotId",
                table: "InventoryLogEntries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "InventoryLogEntries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "GoodsReceipts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PurchaseOrderNumber",
                table: "GoodsReceiptLot",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SublotSize",
                table: "GoodsReceiptLot",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SublotUnit",
                table: "GoodsReceiptLot",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "GoodsReceiptLot",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Receiver",
                table: "GoodsIssues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "GoodsIssues",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PurchaseOrderNumber",
                table: "GoodsIssues",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SublotSize",
                table: "GoodsIssueLot",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SublotUnit",
                table: "GoodsIssueLot",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "RequestedSublotSize",
                table: "GoodsIssueEntry",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "GoodsIssueEntry",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
