using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Education_Software.Migrations
{
    /// <inheritdoc />
    public partial class Statistics_Modified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "true_false_score",
                table: "statistics",
                type: "NUMERIC",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC");

            migrationBuilder.AlterColumn<decimal>(
                name: "specialization_link_score",
                table: "statistics",
                type: "NUMERIC",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC");

            migrationBuilder.AlterColumn<decimal>(
                name: "skills_acquired_score",
                table: "statistics",
                type: "NUMERIC",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC");

            migrationBuilder.AlterColumn<decimal>(
                name: "multiple_choice_score",
                table: "statistics",
                type: "NUMERIC",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC");

            migrationBuilder.AlterColumn<decimal>(
                name: "matching_score",
                table: "statistics",
                type: "NUMERIC",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC");

            migrationBuilder.AlterColumn<decimal>(
                name: "learning_outcomes_score",
                table: "statistics",
                type: "NUMERIC",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC");

            migrationBuilder.AlterColumn<decimal>(
                name: "description_score",
                table: "statistics",
                type: "NUMERIC",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC");

            migrationBuilder.AlterColumn<decimal>(
                name: "completion_score",
                table: "statistics",
                type: "NUMERIC",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "true_false_score",
                table: "statistics",
                type: "NUMERIC",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "specialization_link_score",
                table: "statistics",
                type: "NUMERIC",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "skills_acquired_score",
                table: "statistics",
                type: "NUMERIC",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "multiple_choice_score",
                table: "statistics",
                type: "NUMERIC",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "matching_score",
                table: "statistics",
                type: "NUMERIC",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "learning_outcomes_score",
                table: "statistics",
                type: "NUMERIC",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "description_score",
                table: "statistics",
                type: "NUMERIC",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "completion_score",
                table: "statistics",
                type: "NUMERIC",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC",
                oldNullable: true);
        }
    }
}
