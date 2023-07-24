using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThaiDuongWarehouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class Add_SubLotAdjustmentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SublotAdjustment",
                columns: table => new
                {
                    LotAdjustmentId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeforeQuantityPerLocation = table.Column<double>(type: "float", nullable: false),
                    AfterQuantityPerLocation = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SublotAdjustment", x => new { x.LotAdjustmentId, x.Id });
                    table.ForeignKey(
                        name: "FK_SublotAdjustment_LotAdjustments_LotAdjustmentId",
                        column: x => x.LotAdjustmentId,
                        principalTable: "LotAdjustments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SublotAdjustment");
        }
    }
}
