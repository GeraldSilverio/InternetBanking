using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetBanking.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfPaid",
                table: "Payment");

            migrationBuilder.RenameColumn(
                name: "DateOfPaid",
                table: "Transactions",
                newName: "Date");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Payment",
                type: "Date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Payment");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Transactions",
                newName: "DateOfPaid");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfPaid",
                table: "Payment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
