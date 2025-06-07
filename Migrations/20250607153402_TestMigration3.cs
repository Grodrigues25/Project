using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class TestMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "user");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "user",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "user");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "user",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "user");
        }
    }
}
