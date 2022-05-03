using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnBinhMarket.Migrations
{
    public partial class removemath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaTH",
                table: "ThuongHieux");

            migrationBuilder.DropColumn(
                name: "MaSP",
                table: "SanPhams");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaTH",
                table: "ThuongHieux",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaSP",
                table: "SanPhams",
                type: "int",
                nullable: true);
        }
    }
}
