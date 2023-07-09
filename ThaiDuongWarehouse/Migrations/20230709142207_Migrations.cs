using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThaiDuongWarehouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemClass",
                columns: table => new
                {
                    ItemClassId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemClass", x => x.ItemClassId);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WarehouseName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

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
                name: "GoodsIssues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoodsIssueId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Receiver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsIssues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsIssues_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoodsReceiptId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsReceipts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemClassId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MinimumStockLevel = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: true),
                    SublotSize = table.Column<double>(type: "float", nullable: true),
                    PacketUnit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_ItemClass_ItemClassId",
                        column: x => x.ItemClassId,
                        principalTable: "ItemClass",
                        principalColumn: "ItemClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
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
                name: "FinishedProductIssueEntry",
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
                    table.PrimaryKey("PK_FinishedProductIssueEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinishedProductIssueEntry_FinisedProductIssues_FinishedProductIssueId",
                        column: x => x.FinishedProductIssueId,
                        principalTable: "FinisedProductIssues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinishedProductIssueEntry_Items_ItemId",
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

            migrationBuilder.CreateTable(
                name: "GoodsIssueEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestedQuantity = table.Column<double>(type: "float", nullable: false),
                    GoodsIssueId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsIssueEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsIssueEntry_GoodsIssues_GoodsIssueId",
                        column: x => x.GoodsIssueId,
                        principalTable: "GoodsIssues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsIssueEntry_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceiptLot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoodsReceiptLotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    ProductionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    GoodsReceiptId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceiptLot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsReceiptLot_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReceiptLot_GoodsReceipts_GoodsReceiptId",
                        column: x => x.GoodsReceiptId,
                        principalTable: "GoodsReceipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsReceiptLot_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryLogEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ItemLotId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeforeQuantity = table.Column<double>(type: "float", nullable: false),
                    ChangedQuantity = table.Column<double>(type: "float", nullable: false),
                    ReceivedQuantity = table.Column<double>(type: "float", nullable: false),
                    ShippedQuantity = table.Column<double>(type: "float", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryLogEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryLogEntries_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemLots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsIsolated = table.Column<bool>(type: "bit", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemLots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemLots_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LotAdjustments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BeforeQuantity = table.Column<double>(type: "float", nullable: false),
                    AfterQuantity = table.Column<double>(type: "float", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotAdjustments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotAdjustments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LotAdjustments_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsIssueLot",
                columns: table => new
                {
                    GoodsIssueLotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GoodsIssueEntryId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsIssueLot", x => new { x.GoodsIssueEntryId, x.GoodsIssueLotId });
                    table.ForeignKey(
                        name: "FK_GoodsIssueLot_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsIssueLot_GoodsIssueEntry_GoodsIssueEntryId",
                        column: x => x.GoodsIssueEntryId,
                        principalTable: "GoodsIssueEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemLotLocations",
                columns: table => new
                {
                    ItemLotId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    QuantityPerLocation = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemLotLocations", x => new { x.ItemLotId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_ItemLotLocations_ItemLots_ItemLotId",
                        column: x => x.ItemLotId,
                        principalTable: "ItemLots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemLotLocations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeId",
                table: "Employees",
                column: "EmployeeId",
                unique: true);

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
                name: "IX_FinishedProductIssueEntry_FinishedProductIssueId",
                table: "FinishedProductIssueEntry",
                column: "FinishedProductIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProductIssueEntry_ItemId",
                table: "FinishedProductIssueEntry",
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

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssueEntry_GoodsIssueId",
                table: "GoodsIssueEntry",
                column: "GoodsIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssueEntry_ItemId",
                table: "GoodsIssueEntry",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssueLot_EmployeeId",
                table: "GoodsIssueLot",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssues_EmployeeId",
                table: "GoodsIssues",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssues_GoodsIssueId",
                table: "GoodsIssues",
                column: "GoodsIssueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptLot_EmployeeId",
                table: "GoodsReceiptLot",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptLot_GoodsReceiptId",
                table: "GoodsReceiptLot",
                column: "GoodsReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptLot_GoodsReceiptLotId",
                table: "GoodsReceiptLot",
                column: "GoodsReceiptLotId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptLot_ItemId",
                table: "GoodsReceiptLot",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceipts_EmployeeId",
                table: "GoodsReceipts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceipts_GoodsReceiptId",
                table: "GoodsReceipts",
                column: "GoodsReceiptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLogEntries_ItemId",
                table: "InventoryLogEntries",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemLotLocations_LocationId",
                table: "ItemLotLocations",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemLots_ItemId",
                table: "ItemLots",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemLots_LotId",
                table: "ItemLots",
                column: "LotId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemClassId",
                table: "Items",
                column: "ItemClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemId_Unit",
                table: "Items",
                columns: new[] { "ItemId", "Unit" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_LocationId",
                table: "Locations",
                column: "LocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_WarehouseId",
                table: "Locations",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_LotAdjustments_EmployeeId",
                table: "LotAdjustments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LotAdjustments_ItemId",
                table: "LotAdjustments",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LotAdjustments_LotId",
                table: "LotAdjustments",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_WarehouseId",
                table: "Warehouses",
                column: "WarehouseId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "FinishedProductInventories");

            migrationBuilder.DropTable(
                name: "FinishedProductIssueEntry");

            migrationBuilder.DropTable(
                name: "FinishedProductReceiptEntry");

            migrationBuilder.DropTable(
                name: "GoodsIssueLot");

            migrationBuilder.DropTable(
                name: "GoodsReceiptLot");

            migrationBuilder.DropTable(
                name: "InventoryLogEntries");

            migrationBuilder.DropTable(
                name: "ItemLotLocations");

            migrationBuilder.DropTable(
                name: "LotAdjustments");

            migrationBuilder.DropTable(
                name: "FinisedProductIssues");

            migrationBuilder.DropTable(
                name: "FinishedProductReceipts");

            migrationBuilder.DropTable(
                name: "GoodsIssueEntry");

            migrationBuilder.DropTable(
                name: "GoodsReceipts");

            migrationBuilder.DropTable(
                name: "ItemLots");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "GoodsIssues");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ItemClass");
        }
    }
}
