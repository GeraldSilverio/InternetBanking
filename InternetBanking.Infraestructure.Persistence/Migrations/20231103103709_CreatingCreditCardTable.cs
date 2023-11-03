using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetBanking.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreatingCreditCardTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AccountCode",
                table: "Benefiaciary",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "CreditsCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardNumber = table.Column<int>(type: "int", nullable: false),
                    CreditLimited = table.Column<decimal>(type: "Decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    Available = table.Column<decimal>(type: "Decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    Spent = table.Column<decimal>(type: "Decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditsCard", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditsCard_CardNumber",
                table: "CreditsCard",
                column: "CardNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditsCard");

            migrationBuilder.AlterColumn<string>(
                name: "AccountCode",
                table: "Benefiaciary",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
