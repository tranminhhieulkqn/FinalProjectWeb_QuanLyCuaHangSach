using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyCuaHangSach.Data.Migrations
{
    public partial class AddPropertiesForSach : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CoSan",
                table: "Sach",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Gia",
                table: "Sach",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoSan",
                table: "Sach");

            migrationBuilder.DropColumn(
                name: "Gia",
                table: "Sach");
        }
    }
}
