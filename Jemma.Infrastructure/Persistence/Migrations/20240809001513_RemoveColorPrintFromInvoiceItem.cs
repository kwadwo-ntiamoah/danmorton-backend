using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jemma.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColorPrintFromInvoiceItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "InvoiceItem");

            migrationBuilder.DropColumn(
                name: "Print",
                table: "InvoiceItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "InvoiceItem",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Print",
                table: "InvoiceItem",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
