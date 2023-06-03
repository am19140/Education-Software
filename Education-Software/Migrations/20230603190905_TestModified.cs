using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Education_Software.Migrations
{
    /// <inheritdoc />
    public partial class TestModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tests",
                table: "tests");

            migrationBuilder.DropColumn(
                name: "trials",
                table: "tests");

            migrationBuilder.AlterColumn<bool>(
                name: "score",
                table: "tests",
                type: "BOOLEAN",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AddColumn<string>(
                name: "q_id",
                table: "tests",
                type: "VARCHAR",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tests",
                table: "tests",
                columns: new[] { "username", "test_id", "q_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tests",
                table: "tests");

            migrationBuilder.DropColumn(
                name: "q_id",
                table: "tests");

            migrationBuilder.AlterColumn<int>(
                name: "score",
                table: "tests",
                type: "INT",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "BOOLEAN");

            migrationBuilder.AddColumn<int>(
                name: "trials",
                table: "tests",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tests",
                table: "tests",
                columns: new[] { "username", "test_id" });
        }
    }
}
