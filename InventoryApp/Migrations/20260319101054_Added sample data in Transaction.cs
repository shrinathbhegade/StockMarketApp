using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedsampledatainTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "BuyDate", "PriceAtTransaction", "Quantity", "SellDate", "StockId", "UserId" },
                values: new object[] { 1, new DateTime(2026, 3, 19, 10, 10, 53, 385, DateTimeKind.Utc).AddTicks(1923), 150m, 10m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 1);
        }
    }
}
