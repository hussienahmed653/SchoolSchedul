using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSchedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixSubjectAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectAssignments_ClassSection_ClassSectionId",
                table: "SubjectAssignments");

            migrationBuilder.DropIndex(
                name: "IX_SubjectAssignments_ClassSectionId",
                table: "SubjectAssignments");

            migrationBuilder.DropColumn(
                name: "ClassSectionId",
                table: "SubjectAssignments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassSectionId",
                table: "SubjectAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectAssignments_ClassSectionId",
                table: "SubjectAssignments",
                column: "ClassSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectAssignments_ClassSection_ClassSectionId",
                table: "SubjectAssignments",
                column: "ClassSectionId",
                principalTable: "ClassSection",
                principalColumn: "ClassSectionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
