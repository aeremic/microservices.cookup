using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipes.Microservice.Migrations
{
    /// <inheritdoc />
    public partial class added_recipe_more_details : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Calories",
                table: "Recipes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Complexity",
                table: "Recipes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlateQuantity",
                table: "Recipes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeNeeded",
                table: "Recipes",
                type: "interval",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calories",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Complexity",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "PlateQuantity",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "TimeNeeded",
                table: "Recipes");
        }
    }
}
