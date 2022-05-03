using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnBinhMarket.Migrations
{
    public partial class removemaphanhoi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaPhanHoi",
                table: "PhanHois");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaPhanHoi",
                table: "PhanHois",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
