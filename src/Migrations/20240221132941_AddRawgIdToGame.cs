using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gamelib.Migrations
{
    /// <inheritdoc />
    public partial class AddRawgIdToGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RawgId",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RawgId",
                table: "Games");
        }
    }
}
