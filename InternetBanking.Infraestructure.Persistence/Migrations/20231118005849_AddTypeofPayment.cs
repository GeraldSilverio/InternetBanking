using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetBanking.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTypeofPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypeOfPayment",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOfPayment",
                table: "Payment");
        }
    }
}
