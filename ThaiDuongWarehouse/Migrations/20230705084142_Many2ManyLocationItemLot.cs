using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThaiDuongWarehouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class Many2ManyLocationItemLot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemLots_Locations_LocationId",
                table: "ItemLots");

            migrationBuilder.DropIndex(
                name: "IX_ItemLots_LocationId",
                table: "ItemLots");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "ItemLots");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "GoodsReceiptLot");

            migrationBuilder.CreateTable(
                name: "ItemLotLocation",
                columns: table => new
                {
                    ItemLotsId = table.Column<int>(type: "int", nullable: false),
                    LocationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemLotLocation", x => new { x.ItemLotsId, x.LocationsId });
                    table.ForeignKey(
                        name: "FK_ItemLotLocation_ItemLots_ItemLotsId",
                        column: x => x.ItemLotsId,
                        principalTable: "ItemLots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemLotLocation_Locations_LocationsId",
                        column: x => x.LocationsId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemLotLocation_LocationsId",
                table: "ItemLotLocation",
                column: "LocationsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemLotLocation");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "ItemLots",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationId",
                table: "GoodsReceiptLot",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemLots_LocationId",
                table: "ItemLots",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemLots_Locations_LocationId",
                table: "ItemLots",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }
    }
}
