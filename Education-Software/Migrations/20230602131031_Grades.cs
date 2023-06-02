using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Education_Software.Migrations
{
    /// <inheritdoc />
    public partial class Grades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "possible_answers",
                table: "questionnaire",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "grades",
                columns: table => new
                {
                    username = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    sub_id = table.Column<string>(type: "VARCHAR", maxLength: 10, nullable: false),
                    grade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grades", x => new { x.username, x.sub_id });
                    table.ForeignKey(
                        name: "FK_grades_subjects_sub_id",
                        column: x => x.sub_id,
                        principalTable: "subjects",
                        principalColumn: "sub_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grades_users_username",
                        column: x => x.username,
                        principalTable: "users",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_grades_sub_id",
                table: "grades",
                column: "sub_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "grades");

            migrationBuilder.DropColumn(
                name: "possible_answers",
                table: "questionnaire");
        }
    }
}
