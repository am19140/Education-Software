using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Education_Software.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "progress",
                columns: table => new
                {
                    username = table.Column<string>(type: "VARCHAR", maxLength: 6, nullable: false),
                    test_id = table.Column<string>(type: "VARCHAR", maxLength: 20, nullable: false),
                    sub_id = table.Column<string>(type: "VARCHAR", maxLength: 10, nullable: true),
                    test_type = table.Column<string>(type: "CHAR", maxLength: 1, nullable: false),
                    score = table.Column<decimal>(type: "NUMERIC", nullable: false),
                    time = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_progress", x => new { x.username, x.test_id });
                });

            migrationBuilder.CreateTable(
                name: "questionnaire",
                columns: table => new
                {
                    q_id = table.Column<string>(type: "VARCHAR", maxLength: 50, nullable: false),
                    question = table.Column<string>(type: "TEXT", nullable: false),
                    possible_answers = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questionnaire", x => x.q_id);
                });

            migrationBuilder.CreateTable(
                name: "questions",
                columns: table => new
                {
                    q_id = table.Column<string>(type: "VARCHAR", maxLength: 40, nullable: false),
                    question = table.Column<string>(type: "TEXT", nullable: false),
                    possible_answers = table.Column<string>(type: "TEXT", nullable: false),
                    answer = table.Column<string>(type: "TEXT", nullable: false),
                    sub_id = table.Column<string>(type: "VARCHAR", maxLength: 10, nullable: false),
                    chapter = table.Column<string>(type: "VARCHAR", maxLength: 10, nullable: false),
                    q_type = table.Column<string>(type: "VARCHAR", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questions", x => x.q_id);
                });

            migrationBuilder.CreateTable(
                name: "recommendations",
                columns: table => new
                {
                    username = table.Column<string>(type: "VARCHAR", maxLength: 6, nullable: false),
                    recommendation = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recommendations", x => x.username);
                });

            migrationBuilder.CreateTable(
                name: "statistics",
                columns: table => new
                {
                    username = table.Column<string>(type: "VARCHAR", maxLength: 6, nullable: false),
                    description_score = table.Column<decimal>(type: "NUMERIC", nullable: false),
                    learning_outcomes_score = table.Column<decimal>(type: "NUMERIC", nullable: false),
                    skills_acquired_score = table.Column<decimal>(type: "NUMERIC", nullable: false),
                    specialization_link_score = table.Column<decimal>(type: "NUMERIC", nullable: false),
                    multiple_choice_score = table.Column<decimal>(type: "NUMERIC", nullable: false),
                    true_false_score = table.Column<decimal>(type: "NUMERIC", nullable: false),
                    completion_score = table.Column<decimal>(type: "NUMERIC", nullable: false),
                    matching_score = table.Column<decimal>(type: "NUMERIC", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statistics", x => x.username);
                });

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    sub_id = table.Column<string>(type: "VARCHAR", maxLength: 10, nullable: false),
                    title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    semester = table.Column<decimal>(type: "NUMERIC", nullable: false),
                    sub_type = table.Column<string>(type: "CHAR", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    learning_outcomes = table.Column<string>(type: "TEXT", nullable: false),
                    skills_acquired = table.Column<string>(type: "TEXT", nullable: false),
                    specialization_link = table.Column<string>(type: "TEXT", nullable: false),
                    reading = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.sub_id);
                });

            migrationBuilder.CreateTable(
                name: "tests",
                columns: table => new
                {
                    username = table.Column<string>(type: "VARCHAR", maxLength: 6, nullable: false),
                    test_id = table.Column<string>(type: "VARCHAR", maxLength: 20, nullable: false),
                    q_id = table.Column<string>(type: "VARCHAR", maxLength: 40, nullable: false),
                    test_type = table.Column<string>(type: "CHAR", maxLength: 1, nullable: false),
                    score = table.Column<bool>(type: "BOOLEAN", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tests", x => new { x.username, x.test_id, x.q_id });
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    username = table.Column<string>(type: "text", nullable: false),
                    pass = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    student_state = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    gender = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.username);
                });

            migrationBuilder.CreateTable(
                name: "grades",
                columns: table => new
                {
                    username = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    sub_id = table.Column<string>(type: "VARCHAR", maxLength: 10, nullable: false),
                    grade = table.Column<int>(type: "integer", nullable: true)
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

            migrationBuilder.DropTable(
                name: "progress");

            migrationBuilder.DropTable(
                name: "questionnaire");

            migrationBuilder.DropTable(
                name: "questions");

            migrationBuilder.DropTable(
                name: "recommendations");

            migrationBuilder.DropTable(
                name: "statistics");

            migrationBuilder.DropTable(
                name: "tests");

            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
