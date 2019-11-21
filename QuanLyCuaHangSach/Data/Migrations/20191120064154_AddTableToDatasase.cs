using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyCuaHangSach.Data.Migrations
{
    public partial class AddTableToDatasase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    IDKhachHang = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKhachHang = table.Column<string>(nullable: true),
                    EmailKhachHang = table.Column<string>(nullable: true),
                    SDTKhachHang = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.IDKhachHang);
                });

            migrationBuilder.CreateTable(
                name: "NhaXuatBan",
                columns: table => new
                {
                    IDNhaXuatBan = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNhaXuatBan = table.Column<string>(nullable: true),
                    DiaChi = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ThongTinNguoiDaiDien = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaXuatBan", x => x.IDNhaXuatBan);
                });

            migrationBuilder.CreateTable(
                name: "TacGia",
                columns: table => new
                {
                    IDTacGia = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTacGia = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    GhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TacGia", x => x.IDTacGia);
                });

            migrationBuilder.CreateTable(
                name: "TheLoai",
                columns: table => new
                {
                    IDTheLoai = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTheLoai = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheLoai", x => x.IDTheLoai);
                });

            migrationBuilder.CreateTable(
                name: "GiaoDich",
                columns: table => new
                {
                    IDGiaoDich = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDKhachHang = table.Column<int>(nullable: false),
                    KhachHangIDKhachHang = table.Column<int>(nullable: true),
                    NgayGiaoDich = table.Column<DateTime>(nullable: false),
                    XacNhan = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaoDich", x => x.IDGiaoDich);
                    table.ForeignKey(
                        name: "FK_GiaoDich_KhachHang_KhachHangIDKhachHang",
                        column: x => x.KhachHangIDKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "IDKhachHang",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sach",
                columns: table => new
                {
                    IDSach = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSach = table.Column<string>(nullable: true),
                    IDTacGia = table.Column<int>(nullable: false),
                    IDTheLoai = table.Column<int>(nullable: false),
                    IDNhaXuatBan = table.Column<int>(nullable: false),
                    SoLuongCoSan = table.Column<int>(nullable: false),
                    CoSan = table.Column<bool>(nullable: false),
                    Gia = table.Column<double>(nullable: false),
                    Anh = table.Column<string>(nullable: true),
                    NamXB = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sach", x => x.IDSach);
                    table.ForeignKey(
                        name: "FK_Sach_NhaXuatBan_IDNhaXuatBan",
                        column: x => x.IDNhaXuatBan,
                        principalTable: "NhaXuatBan",
                        principalColumn: "IDNhaXuatBan",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sach_TacGia_IDTacGia",
                        column: x => x.IDTacGia,
                        principalTable: "TacGia",
                        principalColumn: "IDTacGia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sach_TheLoai_IDTheLoai",
                        column: x => x.IDTheLoai,
                        principalTable: "TheLoai",
                        principalColumn: "IDTheLoai",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietGiaoDich",
                columns: table => new
                {
                    IDCTGiaoDich = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDGiaoDich = table.Column<int>(nullable: false),
                    IDSach = table.Column<int>(nullable: false),
                    SoLuongMua = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietGiaoDich", x => x.IDCTGiaoDich);
                    table.ForeignKey(
                        name: "FK_ChiTietGiaoDich_GiaoDich_IDGiaoDich",
                        column: x => x.IDGiaoDich,
                        principalTable: "GiaoDich",
                        principalColumn: "IDGiaoDich",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietGiaoDich_Sach_IDSach",
                        column: x => x.IDSach,
                        principalTable: "Sach",
                        principalColumn: "IDSach",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGiaoDich_IDGiaoDich",
                table: "ChiTietGiaoDich",
                column: "IDGiaoDich");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGiaoDich_IDSach",
                table: "ChiTietGiaoDich",
                column: "IDSach");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoDich_KhachHangIDKhachHang",
                table: "GiaoDich",
                column: "KhachHangIDKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_Sach_IDNhaXuatBan",
                table: "Sach",
                column: "IDNhaXuatBan");

            migrationBuilder.CreateIndex(
                name: "IX_Sach_IDTacGia",
                table: "Sach",
                column: "IDTacGia");

            migrationBuilder.CreateIndex(
                name: "IX_Sach_IDTheLoai",
                table: "Sach",
                column: "IDTheLoai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietGiaoDich");

            migrationBuilder.DropTable(
                name: "GiaoDich");

            migrationBuilder.DropTable(
                name: "Sach");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "NhaXuatBan");

            migrationBuilder.DropTable(
                name: "TacGia");

            migrationBuilder.DropTable(
                name: "TheLoai");
        }
    }
}
