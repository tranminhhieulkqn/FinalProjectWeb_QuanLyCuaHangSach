using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyCuaHangSach.Data.Migrations
{
    public partial class AddTablePhieuNhap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhieuNhap",
                columns: table => new
                {
                    IDPhieuNhap = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDNhanVien = table.Column<string>(nullable: true),
                    IDSach = table.Column<int>(nullable: false),
                    NgayNhap = table.Column<DateTime>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    MoTa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuNhap", x => x.IDPhieuNhap);
                    table.ForeignKey(
                        name: "FK_PhieuNhap_AspNetUsers_IDNhanVien",
                        column: x => x.IDNhanVien,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhieuNhap_Sach_IDSach",
                        column: x => x.IDSach,
                        principalTable: "Sach",
                        principalColumn: "IDSach",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhap_IDNhanVien",
                table: "PhieuNhap",
                column: "IDNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhap_IDSach",
                table: "PhieuNhap",
                column: "IDSach");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhieuNhap");
        }
    }
}
