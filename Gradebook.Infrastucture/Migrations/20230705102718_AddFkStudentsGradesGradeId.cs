using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFkStudentsGradesGradeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                schema: "gradebook",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_GradeId",
                schema: "gradebook",
                table: "Students",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Grades_GradeId",
                schema: "gradebook",
                table: "Students",
                column: "GradeId",
                principalSchema: "gradebook",
                principalTable: "Grades",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Grades_GradeId",
                schema: "gradebook",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_GradeId",
                schema: "gradebook",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "GradeId",
                schema: "gradebook",
                table: "Students");
        }
    }
}
