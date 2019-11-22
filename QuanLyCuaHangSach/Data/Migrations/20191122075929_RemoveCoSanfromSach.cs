using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyCuaHangSach.Data.Migrations
{
    public partial class RemoveCoSanfromSach : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoSan",
                table: "Sach");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CoSan",
                table: "Sach",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
