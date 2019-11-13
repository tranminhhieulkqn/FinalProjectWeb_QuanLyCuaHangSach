using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyCuaHangSach.Data.Migrations
{
    public partial class AddTableToDatabase : Migration
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
                    DiaChi = table.Column<string>(nullable: true),
                    SoDienThoai = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.IDKhachHang);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCap",
                columns: table => new
                {
                    IDNhaCungCap = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNhaCungCap = table.Column<string>(nullable: true),
                    DiaChi = table.Column<string>(nullable: true),
                    SoDienThoai = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCap", x => x.IDNhaCungCap);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    IDNhanVien = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(nullable: true),
                    NgaySinh = table.Column<DateTime>(nullable: false),
                    SoDienThoai = table.Column<string>(nullable: true),
                    Anh = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.IDNhanVien);
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
                name: "PhieuNhap",
                columns: table => new
                {
                    IDPhieuNhap = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDNhanVien = table.Column<int>(nullable: false),
                    IDNhaCungCap = table.Column<int>(nullable: false),
                    NgayNhap = table.Column<DateTime>(nullable: false),
                    KhachHangIDKhachHang = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuNhap", x => x.IDPhieuNhap);
                    table.ForeignKey(
                        name: "FK_PhieuNhap_NhaCungCap_IDNhaCungCap",
                        column: x => x.IDNhaCungCap,
                        principalTable: "NhaCungCap",
                        principalColumn: "IDNhaCungCap",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhieuNhap_NhanVien_IDNhanVien",
                        column: x => x.IDNhanVien,
                        principalTable: "NhanVien",
                        principalColumn: "IDNhanVien",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhieuNhap_KhachHang_KhachHangIDKhachHang",
                        column: x => x.KhachHangIDKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "IDKhachHang",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhieuXuat",
                columns: table => new
                {
                    IDPhieuXuat = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDNhanVien = table.Column<int>(nullable: false),
                    IDKhachHang = table.Column<int>(nullable: false),
                    NgayXuat = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuXuat", x => x.IDPhieuXuat);
                    table.ForeignKey(
                        name: "FK_PhieuXuat_KhachHang_IDKhachHang",
                        column: x => x.IDKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "IDKhachHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhieuXuat_NhanVien_IDNhanVien",
                        column: x => x.IDNhanVien,
                        principalTable: "NhanVien",
                        principalColumn: "IDNhanVien",
                        onDelete: ReferentialAction.Cascade);
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
                name: "CTPhieuNhap",
                columns: table => new
                {
                    IDCTPhieuNhap = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDPhieuNhap = table.Column<int>(nullable: false),
                    IDSach = table.Column<int>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    DonGia = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTPhieuNhap", x => new { x.IDCTPhieuNhap, x.IDPhieuNhap, x.IDSach });
                    table.ForeignKey(
                        name: "FK_CTPhieuNhap_PhieuNhap_IDPhieuNhap",
                        column: x => x.IDPhieuNhap,
                        principalTable: "PhieuNhap",
                        principalColumn: "IDPhieuNhap",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CTPhieuNhap_Sach_IDSach",
                        column: x => x.IDSach,
                        principalTable: "Sach",
                        principalColumn: "IDSach",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CTPhieuXuat",
                columns: table => new
                {
                    IDCTPhieuXuat = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDPhieuXuat = table.Column<int>(nullable: false),
                    IDSach = table.Column<int>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    DonGia = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTPhieuXuat", x => new { x.IDCTPhieuXuat, x.IDPhieuXuat, x.IDSach });
                    table.ForeignKey(
                        name: "FK_CTPhieuXuat_PhieuXuat_IDPhieuXuat",
                        column: x => x.IDPhieuXuat,
                        principalTable: "PhieuXuat",
                        principalColumn: "IDPhieuXuat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CTPhieuXuat_Sach_IDSach",
                        column: x => x.IDSach,
                        principalTable: "Sach",
                        principalColumn: "IDSach",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CTPhieuNhap_IDPhieuNhap",
                table: "CTPhieuNhap",
                column: "IDPhieuNhap");

            migrationBuilder.CreateIndex(
                name: "IX_CTPhieuNhap_IDSach",
                table: "CTPhieuNhap",
                column: "IDSach");

            migrationBuilder.CreateIndex(
                name: "IX_CTPhieuXuat_IDPhieuXuat",
                table: "CTPhieuXuat",
                column: "IDPhieuXuat");

            migrationBuilder.CreateIndex(
                name: "IX_CTPhieuXuat_IDSach",
                table: "CTPhieuXuat",
                column: "IDSach");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhap_IDNhaCungCap",
                table: "PhieuNhap",
                column: "IDNhaCungCap");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhap_IDNhanVien",
                table: "PhieuNhap",
                column: "IDNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhap_KhachHangIDKhachHang",
                table: "PhieuNhap",
                column: "KhachHangIDKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuXuat_IDKhachHang",
                table: "PhieuXuat",
                column: "IDKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuXuat_IDNhanVien",
                table: "PhieuXuat",
                column: "IDNhanVien");

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
                name: "CTPhieuNhap");

            migrationBuilder.DropTable(
                name: "CTPhieuXuat");

            migrationBuilder.DropTable(
                name: "PhieuNhap");

            migrationBuilder.DropTable(
                name: "PhieuXuat");

            migrationBuilder.DropTable(
                name: "Sach");

            migrationBuilder.DropTable(
                name: "NhaCungCap");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "NhaXuatBan");

            migrationBuilder.DropTable(
                name: "TacGia");

            migrationBuilder.DropTable(
                name: "TheLoai");
        }
    }
}
