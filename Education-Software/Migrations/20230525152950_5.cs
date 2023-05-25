using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Education_Software.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "questionnaire",
                columns: table => new
                {
                    q_id = table.Column<string>(type: "VARCHAR", maxLength: 50, nullable: false),
                    question = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questionnaire", x => x.q_id);
                });

            migrationBuilder.CreateTable(
                name: "recommendations",
                columns: table => new
                {
                    username = table.Column<string>(type: "VARCHAR", maxLength: 6, nullable: false),
                    q_id = table.Column<string>(type: "VARCHAR", maxLength: 50, nullable: false),
                    answer = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recommendations", x => new { x.username, x.q_id });
                });

            migrationBuilder.CreateTable(
                name: "specializations",
                columns: table => new
                {
                    spe_id = table.Column<string>(type: "VARCHAR", maxLength: 50, nullable: false),
                    spe_name = table.Column<string>(type: "VARCHAR", maxLength: 50, nullable: false),
                    sub_id = table.Column<string>(type: "VARCHAR", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specializations", x => new { x.spe_id, x.spe_name, x.sub_id });
                });

            migrationBuilder.CreateTable(
                name: "tests",
                columns: table => new
                {
                    username = table.Column<string>(type: "VARCHAR", maxLength: 6, nullable: false),
                    test_id = table.Column<string>(type: "VARCHAR", maxLength: 20, nullable: false),
                    test_type = table.Column<string>(type: "CHAR", maxLength: 1, nullable: false),
                    score = table.Column<int>(type: "INT", nullable: false),
                    trials = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tests", x => new { x.username, x.test_id });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "questionnaire");

            migrationBuilder.DropTable(
                name: "recommendations");

            migrationBuilder.DropTable(
                name: "specializations");

            migrationBuilder.DropTable(
                name: "tests");
        }
    }
}
