using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bangazonBE.Migrations
{
    public partial class updatedMigrations4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userName",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "uId",
                table: "Users",
                newName: "Uid");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "isSeller",
                table: "Users",
                newName: "IsSeller");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Users",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "sellerId",
                table: "Products",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "quantityAvailable",
                table: "Products",
                newName: "QuantityAvailable");

            migrationBuilder.RenameColumn(
                name: "productName",
                table: "Products",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "pricePerUnit",
                table: "Products",
                newName: "PricePerUnit");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "PaymentTypes",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "paymentTypeId",
                table: "Orders",
                newName: "PaymentTypeId");

            migrationBuilder.RenameColumn(
                name: "isComplete",
                table: "Orders",
                newName: "IsComplete");

            migrationBuilder.RenameColumn(
                name: "customerId",
                table: "Orders",
                newName: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "userName");

            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "Users",
                newName: "uId");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "IsSeller",
                table: "Users",
                newName: "isSeller");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Users",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Products",
                newName: "sellerId");

            migrationBuilder.RenameColumn(
                name: "QuantityAvailable",
                table: "Products",
                newName: "quantityAvailable");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "productName");

            migrationBuilder.RenameColumn(
                name: "PricePerUnit",
                table: "Products",
                newName: "pricePerUnit");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Products",
                newName: "categoryId");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "PaymentTypes",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "PaymentTypeId",
                table: "Orders",
                newName: "paymentTypeId");

            migrationBuilder.RenameColumn(
                name: "IsComplete",
                table: "Orders",
                newName: "isComplete");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Orders",
                newName: "customerId");
        }
    }
}
