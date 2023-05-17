using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Education_Software.Migrations
{
    /// <inheritdoc />
    public partial class _3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_subjects",
                table: "subjects");

            migrationBuilder.RenameTable(
                name: "subjects",
                newName: "SubjectModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectModel",
                table: "SubjectModel",
                column: "sub_id");

            migrationBuilder.CreateTable(
                name: "QuestionModel",
                columns: table => new
                {
                    q_id = table.Column<string>(type: "VARCHAR", maxLength: 40, nullable: false),
                    question = table.Column<string>(type: "TEXT", nullable: false),
                    answer = table.Column<string>(type: "TEXT", nullable: false),
                    sub_id = table.Column<string>(type: "VARCHAR", maxLength: 10, nullable: false),
                    chapter = table.Column<string>(type: "VARCHAR", maxLength: 10, nullable: false),
                    q_type = table.Column<string>(type: "VARCHAR", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionModel", x => x.q_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectModel",
                table: "SubjectModel");

            migrationBuilder.RenameTable(
                name: "SubjectModel",
                newName: "subjects");

            migrationBuilder.AddPrimaryKey(
                name: "PK_subjects",
                table: "subjects",
                column: "sub_id");
        }
    }
}
