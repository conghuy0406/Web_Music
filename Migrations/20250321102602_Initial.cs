using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Music.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CaSis",
                columns: table => new
                {
                    MaCaSi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenCaSi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TieuSu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaSis", x => x.MaCaSi);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    MaNguoiDung = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaiTro = table.Column<int>(type: "int", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.MaNguoiDung);
                });

            migrationBuilder.CreateTable(
                name: "TheLoais",
                columns: table => new
                {
                    MaTheLoai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTheLoai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheLoais", x => x.MaTheLoai);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    MaAlbum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenAlbum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaCaSi = table.Column<int>(type: "int", nullable: false),
                    AnhBia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayPhatHanh = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.MaAlbum);
                    table.ForeignKey(
                        name: "FK_Albums_CaSis_MaCaSi",
                        column: x => x.MaCaSi,
                        principalTable: "CaSis",
                        principalColumn: "MaCaSi",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaiHats",
                columns: table => new
                {
                    MaBaiHat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenBaiHat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaCaSi = table.Column<int>(type: "int", nullable: false),
                    MaAlbum = table.Column<int>(type: "int", nullable: true),
                    MaTheLoai = table.Column<int>(type: "int", nullable: false),
                    ThoiLuong = table.Column<TimeOnly>(type: "time", nullable: false),
                    DuongDanFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LuotNghe = table.Column<int>(type: "int", nullable: true),
                    NgayThem = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaiHats", x => x.MaBaiHat);
                    table.ForeignKey(
                        name: "FK_BaiHats_Albums_MaAlbum",
                        column: x => x.MaAlbum,
                        principalTable: "Albums",
                        principalColumn: "MaAlbum");
                    table.ForeignKey(
                        name: "FK_BaiHats_CaSis_MaCaSi",
                        column: x => x.MaCaSi,
                        principalTable: "CaSis",
                        principalColumn: "MaCaSi",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaiHats_TheLoais_MaTheLoai",
                        column: x => x.MaTheLoai,
                        principalTable: "TheLoais",
                        principalColumn: "MaTheLoai",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_MaCaSi",
                table: "Albums",
                column: "MaCaSi");

            migrationBuilder.CreateIndex(
                name: "IX_BaiHats_MaAlbum",
                table: "BaiHats",
                column: "MaAlbum");

            migrationBuilder.CreateIndex(
                name: "IX_BaiHats_MaCaSi",
                table: "BaiHats",
                column: "MaCaSi");

            migrationBuilder.CreateIndex(
                name: "IX_BaiHats_MaTheLoai",
                table: "BaiHats",
                column: "MaTheLoai");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaiHats");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "TheLoais");

            migrationBuilder.DropTable(
                name: "CaSis");
        }
    }
}
