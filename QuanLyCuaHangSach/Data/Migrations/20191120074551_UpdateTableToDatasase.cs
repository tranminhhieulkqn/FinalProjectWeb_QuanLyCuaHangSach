using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyCuaHangSach.Data.Migrations
{
    public partial class UpdateTableToDatasase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiaoDich_KhachHang_KhachHangIDKhachHang",
                table: "GiaoDich");

            migrationBuilder.DropIndex(
                name: "IX_GiaoDich_KhachHangIDKhachHang",
                table: "GiaoDich");

            migrationBuilder.DropColumn(
                name: "KhachHangIDKhachHang",
                table: "GiaoDich");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoDich_IDKhachHang",
                table: "GiaoDich",
                column: "IDKhachHang");

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoDich_KhachHang_IDKhachHang",
                table: "GiaoDich",
                column: "IDKhachHang",
                principalTable: "KhachHang",
                principalColumn: "IDKhachHang",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiaoDich_KhachHang_IDKhachHang",
                table: "GiaoDich");

            migrationBuilder.DropIndex(
                name: "IX_GiaoDich_IDKhachHang",
                table: "GiaoDich");

            migrationBuilder.AddColumn<int>(
                name: "KhachHangIDKhachHang",
                table: "GiaoDich",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GiaoDich_KhachHangIDKhachHang",
                table: "GiaoDich",
                column: "KhachHangIDKhachHang");

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoDich_KhachHang_KhachHangIDKhachHang",
                table: "GiaoDich",
                column: "KhachHangIDKhachHang",
                principalTable: "KhachHang",
                principalColumn: "IDKhachHang",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
