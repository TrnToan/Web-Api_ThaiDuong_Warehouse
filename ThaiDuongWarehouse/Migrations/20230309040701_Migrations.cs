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
                name: "GoodsIssues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoodsIssueId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Receiver = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsIssues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoodsReceiptId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PurchaseOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceipts", x => x.Id);
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
                name: "Unit",
                columns: table => new
                {
                    UnitName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.UnitName);
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
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemClassId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinimumStockLevel = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Items_Unit_UnitName",
                        column: x => x.UnitName,
                        principalTable: "Unit",
                        principalColumn: "UnitName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GoodsIssueEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoodsIssueId = table.Column<int>(type: "int", nullable: false),
                    RequestedSublotSize = table.Column<double>(type: "float", nullable: true),
                    RequestedQuantity = table.Column<double>(type: "float", nullable: false),
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
                    GoodsReceiptLotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    SublotSize = table.Column<double>(type: "float", nullable: true),
                    PurchaseOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GoodsReceiptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceiptLot", x => x.GoodsReceiptLotId);
                    table.ForeignKey(
                        name: "FK_GoodsReceiptLot_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    ItemLotId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BeforeQuantity = table.Column<double>(type: "float", nullable: false),
                    ChangedQuantity = table.Column<double>(type: "float", nullable: false)
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
                name: "LotAdjustments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    NewPurchaseOrderNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OldPurchaseOrderNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeforeQuantity = table.Column<double>(type: "float", nullable: false),
                    AfterQuantity = table.Column<double>(type: "float", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "ItemLots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    IsIsolated = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    SublotSize = table.Column<double>(type: "float", nullable: false),
                    PurchaseOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_ItemLots_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsIssueLot",
                columns: table => new
                {
                    GoodsIssueLotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    SublotSize = table.Column<double>(type: "float", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoodsIssueEntryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsIssueLot", x => x.GoodsIssueLotId);
                    table.ForeignKey(
                        name: "FK_GoodsIssueLot_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsIssueLot_GoodsIssueEntry_GoodsIssueEntryId",
                        column: x => x.GoodsIssueEntryId,
                        principalTable: "GoodsIssueEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeId",
                table: "Employees",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssueEntry_GoodsIssueId",
                table: "GoodsIssueEntry",
                column: "GoodsIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssueEntry_ItemId",
                table: "GoodsIssueEntry",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssueLot_EmployeeId",
                table: "GoodsIssueLot",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssueLot_GoodsIssueEntryId",
                table: "GoodsIssueLot",
                column: "GoodsIssueEntryId");

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
                name: "IX_GoodsReceiptLot_ItemId",
                table: "GoodsReceiptLot",
                column: "ItemId",
                unique: true);

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
                name: "IX_ItemLots_ItemId",
                table: "ItemLots",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemLots_LocationId",
                table: "ItemLots",
                column: "LocationId");

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
                name: "IX_Items_ItemId",
                table: "Items",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_UnitName",
                table: "Items",
                column: "UnitName");

            migrationBuilder.CreateIndex(
                name: "IX_Location_LocationId",
                table: "Location",
                column: "LocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_WarehouseId",
                table: "Location",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_LotAdjustments_EmployeeId",
                table: "LotAdjustments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LotAdjustments_ItemId",
                table: "LotAdjustments",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LotAdjustments_LotId",
                table: "LotAdjustments",
                column: "LotId",
                unique: true);

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
                name: "GoodsIssueLot");

            migrationBuilder.DropTable(
                name: "GoodsReceiptLot");

            migrationBuilder.DropTable(
                name: "InventoryLogEntries");

            migrationBuilder.DropTable(
                name: "ItemLots");

            migrationBuilder.DropTable(
                name: "LotAdjustments");

            migrationBuilder.DropTable(
                name: "GoodsIssueEntry");

            migrationBuilder.DropTable(
                name: "GoodsReceipts");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "GoodsIssues");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "ItemClass");

            migrationBuilder.DropTable(
                name: "Unit");
        }
    }
}
