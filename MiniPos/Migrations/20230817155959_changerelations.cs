using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniPos.Migrations
{
    /// <inheritdoc />
    public partial class changerelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_Invoice_InvoiceId",
                table: "InvoiceLine");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceLine_ProductID",
                table: "InvoiceLine");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "InvoiceLine",
                newName: "InvoiceID");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceLine_InvoiceId",
                table: "InvoiceLine",
                newName: "IX_InvoiceLine_InvoiceID");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceID",
                table: "InvoiceLine",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_ProductID",
                table: "InvoiceLine",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_Invoice_InvoiceID",
                table: "InvoiceLine",
                column: "InvoiceID",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_Invoice_InvoiceID",
                table: "InvoiceLine");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceLine_ProductID",
                table: "InvoiceLine");

            migrationBuilder.RenameColumn(
                name: "InvoiceID",
                table: "InvoiceLine",
                newName: "InvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceLine_InvoiceID",
                table: "InvoiceLine",
                newName: "IX_InvoiceLine_InvoiceId");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "InvoiceLine",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_ProductID",
                table: "InvoiceLine",
                column: "ProductID",
                unique: true,
                filter: "[ProductID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_Invoice_InvoiceId",
                table: "InvoiceLine",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id");
        }
    }
}
