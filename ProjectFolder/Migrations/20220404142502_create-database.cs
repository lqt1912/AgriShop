using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriShop.Migrations
{
    public partial class createdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhMucs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaDanhMuc = table.Column<int>(type: "int", nullable: false),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GioHangs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaGioHang = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenTaiKhoan = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GioHangs_AspNetUsers_TenTaiKhoan",
                        column: x => x.TenTaiKhoan,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LienHes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaLienHe = table.Column<int>(type: "int", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LienHes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhanHois",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaPhanHoi = table.Column<int>(type: "int", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanHois", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThuongHieux",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaTH = table.Column<int>(type: "int", nullable: false),
                    TenHuongHieu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuongHieux", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TinTucs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaTinTuc = table.Column<int>(type: "int", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTaNgan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTaChiTiet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenTaiKhoan = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinTucs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TinTucs_AspNetUsers_TenTaiKhoan",
                        column: x => x.TenTaiKhoan,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaHoaDon = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhiShip = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChuY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaGioHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoaDons_GioHangs_MaGioHang",
                        column: x => x.MaGioHang,
                        principalTable: "GioHangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPhams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaSP = table.Column<int>(type: "int", nullable: false),
                    TenSP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrongLuong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaDanhMuc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaTH = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SanPhams_DanhMucs_MaDanhMuc",
                        column: x => x.MaDanhMuc,
                        principalTable: "DanhMucs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPhams_ThuongHieux_MaTH",
                        column: x => x.MaTH,
                        principalTable: "ThuongHieux",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietGioHangs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaSP = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaGioHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietGioHangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietGioHangs_GioHangs_MaGioHang",
                        column: x => x.MaGioHang,
                        principalTable: "GioHangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietGioHangs_SanPhams_MaSP",
                        column: x => x.MaSP,
                        principalTable: "SanPhams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGioHangs_MaGioHang",
                table: "ChiTietGioHangs",
                column: "MaGioHang");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGioHangs_MaSP",
                table: "ChiTietGioHangs",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangs_TenTaiKhoan",
                table: "GioHangs",
                column: "TenTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_MaGioHang",
                table: "HoaDons",
                column: "MaGioHang");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_MaDanhMuc",
                table: "SanPhams",
                column: "MaDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_MaTH",
                table: "SanPhams",
                column: "MaTH");

            migrationBuilder.CreateIndex(
                name: "IX_TinTucs_TenTaiKhoan",
                table: "TinTucs",
                column: "TenTaiKhoan");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietGioHangs");

            migrationBuilder.DropTable(
                name: "HoaDons");

            migrationBuilder.DropTable(
                name: "LienHes");

            migrationBuilder.DropTable(
                name: "PhanHois");

            migrationBuilder.DropTable(
                name: "TinTucs");

            migrationBuilder.DropTable(
                name: "SanPhams");

            migrationBuilder.DropTable(
                name: "GioHangs");

            migrationBuilder.DropTable(
                name: "DanhMucs");

            migrationBuilder.DropTable(
                name: "ThuongHieux");
        }
    }
}
