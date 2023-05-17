using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Education_Software.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectModel",
                table: "SubjectModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionModel",
                table: "QuestionModel");

            migrationBuilder.RenameTable(
                name: "SubjectModel",
                newName: "subjects");

            migrationBuilder.RenameTable(
                name: "QuestionModel",
                newName: "questions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_subjects",
                table: "subjects",
                column: "sub_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_questions",
                table: "questions",
                column: "q_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_subjects",
                table: "subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_questions",
                table: "questions");

            migrationBuilder.RenameTable(
                name: "subjects",
                newName: "SubjectModel");

            migrationBuilder.RenameTable(
                name: "questions",
                newName: "QuestionModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectModel",
                table: "SubjectModel",
                column: "sub_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionModel",
                table: "QuestionModel",
                column: "q_id");
        }
    }
}
