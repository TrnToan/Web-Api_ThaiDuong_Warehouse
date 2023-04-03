using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThaiDuongWarehouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnitTable_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Unit_UnitName",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_UnitName",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "UnitName",
                table: "Items",
                newName: "Unit");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "LotAdjustments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "ItemLots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "InventoryLogEntries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "GoodsReceiptLot",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "GoodsIssueEntry",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemId_Unit",
                table: "Items",
                columns: new[] { "ItemId", "Unit" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Items_ItemId_Unit",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "LotAdjustments");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "ItemLots");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "InventoryLogEntries");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "GoodsReceiptLot");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "GoodsIssueEntry");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "Items",
                newName: "UnitName");

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

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemId",
                table: "Items",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_UnitName",
                table: "Items",
                column: "UnitName");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Unit_UnitName",
                table: "Items",
                column: "UnitName",
                principalTable: "Unit",
                principalColumn: "UnitName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
