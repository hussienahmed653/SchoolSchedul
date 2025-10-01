using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSchedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSubjectAssignmentIdToTeacherAssignmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeacherAssignment_TeacherId_ClassSectionId",
                table: "TeacherAssignment");

            migrationBuilder.AddColumn<int>(
                name: "SubjectAssignmentId",
                table: "TeacherAssignment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignment_SubjectAssignmentId",
                table: "TeacherAssignment",
                column: "SubjectAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignment_TeacherId_SubjectAssignmentId_ClassSectionId",
                table: "TeacherAssignment",
                columns: new[] { "TeacherId", "SubjectAssignmentId", "ClassSectionId" },
                unique: true,
                filter: "[SubjectAssignmentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherAssignment_SubjectAssignments_SubjectAssignmentId",
                table: "TeacherAssignment",
                column: "SubjectAssignmentId",
                principalTable: "SubjectAssignments",
                principalColumn: "SubjectAssignmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherAssignment_SubjectAssignments_SubjectAssignmentId",
                table: "TeacherAssignment");

            migrationBuilder.DropIndex(
                name: "IX_TeacherAssignment_SubjectAssignmentId",
                table: "TeacherAssignment");

            migrationBuilder.DropIndex(
                name: "IX_TeacherAssignment_TeacherId_SubjectAssignmentId_ClassSectionId",
                table: "TeacherAssignment");

            migrationBuilder.DropColumn(
                name: "SubjectAssignmentId",
                table: "TeacherAssignment");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignment_TeacherId_ClassSectionId",
                table: "TeacherAssignment",
                columns: new[] { "TeacherId", "ClassSectionId" },
                unique: true);
        }
    }
}
