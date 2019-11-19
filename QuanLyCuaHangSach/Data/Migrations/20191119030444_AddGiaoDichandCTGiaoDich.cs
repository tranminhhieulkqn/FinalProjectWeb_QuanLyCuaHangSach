using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyCuaHangSach.Data.Migrations
{
    public partial class AddGiaoDichandCTGiaoDich : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "GiaoDich",
                columns: table => new
                {
                    IDGiaoDich = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayGiaoDich = table.Column<DateTime>(nullable: false),
                    TenKhachHang = table.Column<string>(nullable: true),
                    SDTKhachHang = table.Column<string>(nullable: true),
                    EmailKhachHang = table.Column<string>(nullable: true),
                    XacNhan = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaoDich", x => x.IDGiaoDich);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietGiaoDich",
                columns: table => new
                {
                    IDCTGiaoDich = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDGiaoDich = table.Column<int>(nullable: false),
                    IDSach = table.Column<int>(nullable: false)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietGiaoDich");

            migrationBuilder.DropTable(
                name: "GiaoDich");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
