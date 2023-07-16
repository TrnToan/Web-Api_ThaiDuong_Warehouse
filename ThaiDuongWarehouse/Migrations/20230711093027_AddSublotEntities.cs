using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThaiDuongWarehouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSublotEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SublotSize",
                table: "Items",
                newName: "PacketSize");

            migrationBuilder.CreateTable(
                name: "GoodsIssueSublot",
                columns: table => new
                {
                    LocationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QuantityPerLocation = table.Column<double>(type: "float", nullable: false),
                    GoodsIssueLotGoodsIssueEntryId = table.Column<int>(type: "int", nullable: false),
                    GoodsIssueLotId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsIssueSublot", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_GoodsIssueSublot_GoodsIssueLot_GoodsIssueLotGoodsIssueEntryId_GoodsIssueLotId",
                        columns: x => new { x.GoodsIssueLotGoodsIssueEntryId, x.GoodsIssueLotId },
                        principalTable: "GoodsIssueLot",
                        principalColumns: new[] { "GoodsIssueEntryId", "GoodsIssueLotId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceiptSublot",
                columns: table => new
                {
                    LocationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QuantityPerLocation = table.Column<double>(type: "float", nullable: false),
                    GoodsReceiptLotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceiptSublot", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_GoodsReceiptSublot_GoodsReceiptLot_GoodsReceiptLotId",
                        column: x => x.GoodsReceiptLotId,
                        principalTable: "GoodsReceiptLot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssueSublot_GoodsIssueLotGoodsIssueEntryId_GoodsIssueLotId",
                table: "GoodsIssueSublot",
                columns: new[] { "GoodsIssueLotGoodsIssueEntryId", "GoodsIssueLotId" });

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptSublot_GoodsReceiptLotId",
                table: "GoodsReceiptSublot",
                column: "GoodsReceiptLotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodsIssueSublot");

            migrationBuilder.DropTable(
                name: "GoodsReceiptSublot");

            migrationBuilder.RenameColumn(
                name: "PacketSize",
                table: "Items",
                newName: "SublotSize");
        }
    }
}
