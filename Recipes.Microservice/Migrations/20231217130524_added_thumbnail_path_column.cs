using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipes.Microservice.Migrations
{
    /// <inheritdoc />
    public partial class added_thumbnail_path_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbnailPath",
                table: "Recipes",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailPath",
                table: "Recipes");
        }
    }
}
