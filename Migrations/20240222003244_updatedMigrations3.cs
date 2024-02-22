using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bangazonBE.Migrations
{
    public partial class updatedMigrations3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    ordersId = table.Column<int>(type: "integer", nullable: false),
                    productsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => new { x.ordersId, x.productsId });
                    table.ForeignKey(
                        name: "FK_OrderProduct_Orders_ordersId",
                        column: x => x.ordersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Products_productsId",
                        column: x => x.productsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_productsId",
                table: "OrderProduct",
                column: "productsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProduct");
        }
    }
}
