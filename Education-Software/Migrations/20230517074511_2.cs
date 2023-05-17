using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Education_Software.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "username");

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    sub_id = table.Column<string>(type: "VARCHAR", maxLength: 10, nullable: false),
                    title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    semester = table.Column<decimal>(type: "NUMERIC", nullable: false),
                    sub_type = table.Column<string>(type: "CHAR", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    description_reading = table.Column<int>(type: "INT", nullable: false),
                    learning_outcomes = table.Column<string>(type: "TEXT", nullable: false),
                    learning_outcomes_reading = table.Column<int>(type: "INT", nullable: false),
                    skills_acquired = table.Column<string>(type: "TEXT", nullable: false),
                    skills_acquired_reading = table.Column<int>(type: "INT", nullable: false),
                    specialization_link = table.Column<string>(type: "TEXT", nullable: false),
                    specialization_link_reading = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.sub_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "username");
        }
    }
}
