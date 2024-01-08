using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Users.Microservice.Migrations
{
    /// <inheritdoc />
    public partial class added_username_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Users",
                newName: "ImageFullPath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "ImageFullPath",
                table: "Users",
                newName: "ImagePath");

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Users",
                type: "text",
                nullable: true);
        }
    }
}
