using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerFantasy.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSchema3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "playerImageURL",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "playerImageURL",
                table: "Players");
        }
    }
}
