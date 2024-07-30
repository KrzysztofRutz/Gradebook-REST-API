using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradebook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabaseGradebookDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "gradebook");

            migrationBuilder.CreateTable(
                name: "Grades",
                schema: "gradebook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(2,1)", precision: 2, scale: 1, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                schema: "gradebook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    YearEnrolled = table.Column<int>(type: "int", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: true),
                    TypeOfStudies = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Grades_GradeId",
                        column: x => x.GradeId,
                        principalSchema: "gradebook",
                        principalTable: "Grades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "gradebook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreetName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    StreetNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "gradebook",
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StudentId",
                schema: "gradebook",
                table: "Addresses",
                column: "StudentId",
                unique: true,
                filter: "[StudentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_Name_Value",
                schema: "gradebook",
                table: "Grades",
                columns: new[] { "Name", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_Email",
                schema: "gradebook",
                table: "Students",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_GradeId",
                schema: "gradebook",
                table: "Students",
                column: "GradeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "gradebook");

            migrationBuilder.DropTable(
                name: "Students",
                schema: "gradebook");

            migrationBuilder.DropTable(
                name: "Grades",
                schema: "gradebook");
        }
    }
}
