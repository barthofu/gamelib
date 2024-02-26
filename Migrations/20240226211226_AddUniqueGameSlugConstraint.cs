using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gamelib.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueGameSlugConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Games_RawgId",
                table: "Games",
                column: "RawgId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_Slug",
                table: "Games",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Games_RawgId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_Slug",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Games");
        }
    }
}
