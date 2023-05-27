using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Education_Software.Migrations
{
    /// <inheritdoc />
    public partial class _6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description_reading",
                table: "subjects");

            migrationBuilder.DropColumn(
                name: "learning_outcomes_reading",
                table: "subjects");

            migrationBuilder.DropColumn(
                name: "skills_acquired_reading",
                table: "subjects");

            migrationBuilder.RenameColumn(
                name: "specialization_link_reading",
                table: "subjects",
                newName: "reading");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "reading",
                table: "subjects",
                newName: "specialization_link_reading");

            migrationBuilder.AddColumn<int>(
                name: "description_reading",
                table: "subjects",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "learning_outcomes_reading",
                table: "subjects",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "skills_acquired_reading",
                table: "subjects",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }
    }
}
