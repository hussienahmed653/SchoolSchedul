using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSchedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherAssignment_Grades_GradeId",
                table: "TeacherAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherAssignment_Subjects_SubjectId",
                table: "TeacherAssignment");

            migrationBuilder.DropIndex(
                name: "IX_TeacherAssignment_GradeId",
                table: "TeacherAssignment");

            migrationBuilder.DropIndex(
                name: "IX_TeacherAssignment_SubjectId",
                table: "TeacherAssignment");

            migrationBuilder.DropIndex(
                name: "IX_TeacherAssignment_TeacherId_SubjectId_GradeId_ClassSectionId",
                table: "TeacherAssignment");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "TeacherAssignment");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "TeacherAssignment");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignment_TeacherId_ClassSectionId",
                table: "TeacherAssignment",
                columns: new[] { "TeacherId", "ClassSectionId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeacherAssignment_TeacherId_ClassSectionId",
                table: "TeacherAssignment");

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "TeacherAssignment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "TeacherAssignment",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherAssignment_Grades_GradeId",
                table: "TeacherAssignment",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherAssignment_Subjects_SubjectId",
                table: "TeacherAssignment",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId");
        }
    }
}
