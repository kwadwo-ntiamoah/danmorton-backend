using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jemma.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalAmount_Amount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    TotalAmount_Currency = table.Column<string>(type: "text", nullable: false),
                    Discount = table.Column<double>(type: "double precision", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "text", nullable: false),
                    AmountPaid_Amount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    AmountPaid_Currency = table.Column<string>(type: "text", nullable: false),
                    PaymentStatus = table.Column<int>(type: "integer", nullable: false),
                    BillTo_Name = table.Column<string>(type: "text", nullable: false),
                    BillTo_Contact = table.Column<string>(type: "text", nullable: false),
                    BillTo_Address = table.Column<string>(type: "text", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AmountPaid_Amount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    AmountPaid_Currency = table.Column<string>(type: "text", nullable: false),
                    PaidBy = table.Column<string>(type: "text", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceType = table.Column<string>(type: "text", nullable: false),
                    ServiceAmount_Amount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    ServiceAmount_Currency = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    Print = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_InvoiceNumber",
                table: "Invoice",
                column: "InvoiceNumber");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_InvoiceId",
                table: "InvoiceItem",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_Name",
                table: "InvoiceItem",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceItem");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Invoice");
        }
    }
}
