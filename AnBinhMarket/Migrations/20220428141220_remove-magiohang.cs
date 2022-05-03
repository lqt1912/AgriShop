using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnBinhMarket.Migrations
{
    public partial class removemagiohang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaGioHang",
                table: "GioHangs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaGioHang",
                table: "GioHangs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
