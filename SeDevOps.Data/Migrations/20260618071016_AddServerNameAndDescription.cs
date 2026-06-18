using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeDevOps.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddServerNameAndDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Servers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Servers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Servers");
        }
    }
}
