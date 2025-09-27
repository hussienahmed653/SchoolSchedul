using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSchedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTeacherAssignmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeacherAssignment",
                columns: table => new
                {
                    TeacherAssignmentId = table.Column<int>(type: "int", nullable: false),
                    TeacherAssignmentGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    ClassSectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherAssignment", x => x.TeacherAssignmentId);
                    table.ForeignKey(
                        name: "FK_TeacherAssignment_ClassSection_ClassSectionId",
                        column: x => x.ClassSectionId,
                        principalTable: "ClassSection",
                        principalColumn: "ClassSectionId");
                    table.ForeignKey(
                        name: "FK_TeacherAssignment_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "GradeId");
                    table.ForeignKey(
                        name: "FK_TeacherAssignment_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId");
                    table.ForeignKey(
                        name: "FK_TeacherAssignment_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignment_ClassSectionId",
                table: "TeacherAssignment",
                column: "ClassSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignment_GradeId",
                table: "TeacherAssignment",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignment_SubjectId",
                table: "TeacherAssignment",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignment_TeacherId_SubjectId_GradeId_ClassSectionId",
                table: "TeacherAssignment",
                columns: new[] { "TeacherId", "SubjectId", "GradeId", "ClassSectionId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherAssignment");
        }
    }
}
