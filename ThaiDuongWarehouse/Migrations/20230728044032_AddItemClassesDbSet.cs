using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThaiDuongWarehouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddItemClassesDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemClass_ItemClassId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemClass",
                table: "ItemClass");

            migrationBuilder.RenameTable(
                name: "ItemClass",
                newName: "ItemClasses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemClasses",
                table: "ItemClasses",
                column: "ItemClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemClasses_ItemClassId",
                table: "Items",
                column: "ItemClassId",
                principalTable: "ItemClasses",
                principalColumn: "ItemClassId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemClasses_ItemClassId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemClasses",
                table: "ItemClasses");

            migrationBuilder.RenameTable(
                name: "ItemClasses",
                newName: "ItemClass");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemClass",
                table: "ItemClass",
                column: "ItemClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemClass_ItemClassId",
                table: "Items",
                column: "ItemClassId",
                principalTable: "ItemClass",
                principalColumn: "ItemClassId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
