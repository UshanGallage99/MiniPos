using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniPos.Migrations
{
    /// <inheritdoc />
    public partial class contactperson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TownID",
                table: "Customers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ContactPerson",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "ContactPerson",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_TownID",
                table: "Customers",
                column: "TownID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPerson_CustomerID",
                table: "ContactPerson",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPerson_Customers_CustomerID",
                table: "ContactPerson",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Town_TownID",
                table: "Customers",
                column: "TownID",
                principalTable: "Town",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactPerson_Customers_CustomerID",
                table: "ContactPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Town_TownID",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_TownID",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_ContactPerson_CustomerID",
                table: "ContactPerson");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ContactPerson");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "ContactPerson");

            migrationBuilder.AlterColumn<int>(
                name: "TownID",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
