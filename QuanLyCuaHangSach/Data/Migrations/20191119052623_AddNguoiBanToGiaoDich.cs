using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyCuaHangSach.Data.Migrations
{
    public partial class AddNguoiBanToGiaoDich : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IDNguoiBan",
                table: "GiaoDich",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GiaoDich_IDNguoiBan",
                table: "GiaoDich",
                column: "IDNguoiBan");

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoDich_AspNetUsers_IDNguoiBan",
                table: "GiaoDich",
                column: "IDNguoiBan",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiaoDich_AspNetUsers_IDNguoiBan",
                table: "GiaoDich");

            migrationBuilder.DropIndex(
                name: "IX_GiaoDich_IDNguoiBan",
                table: "GiaoDich");

            migrationBuilder.DropColumn(
                name: "IDNguoiBan",
                table: "GiaoDich");
        }
    }
}
