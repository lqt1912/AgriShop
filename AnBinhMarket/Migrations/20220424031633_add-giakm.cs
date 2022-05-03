using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnBinhMarket.Migrations
{
    public partial class addgiakm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "GiaKM",
                table: "SanPhams",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GiaKM",
                table: "SanPhams");
        }
    }
}
