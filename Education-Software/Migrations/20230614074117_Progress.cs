using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Education_Software.Migrations
{
    /// <inheritdoc />
    public partial class Progress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "specializations");

            migrationBuilder.CreateTable(
                name: "progress",
                columns: table => new
                {
                    username = table.Column<string>(type: "VARCHAR", maxLength: 6, nullable: false),
                    test_id = table.Column<string>(type: "VARCHAR", maxLength: 20, nullable: false),
                    sub_id = table.Column<string>(type: "VARCHAR", maxLength: 10, nullable: true),
                    test_type = table.Column<string>(type: "CHAR", maxLength: 1, nullable: false),
                    score = table.Column<decimal>(type: "NUMERIC", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_progress", x => new { x.username, x.test_id });
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "progress");

            migrationBuilder.DropTable(
                name: "statistics");

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
        }
    }
}
