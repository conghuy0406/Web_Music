using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Music.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAlbumTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnhBia",
                table: "BaiHats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnhBia",
                table: "BaiHats");
        }
    }
}
