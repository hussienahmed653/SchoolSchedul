using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSchedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClassSectionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassSectionId",
                table: "SubjectAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ClassSection",
                columns: table => new
                {
                    ClassSectionId = table.Column<int>(type: "int", nullable: false),
                    ClassSectionGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    SectionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSection", x => x.ClassSectionId);
                    table.ForeignKey(
                        name: "FK_ClassSection_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "GradeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectAssignments_ClassSectionId",
                table: "SubjectAssignments",
                column: "ClassSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSection_GradeId_SectionName",
                table: "ClassSection",
                columns: new[] { "GradeId", "SectionName" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectAssignments_ClassSection_ClassSectionId",
                table: "SubjectAssignments",
                column: "ClassSectionId",
                principalTable: "ClassSection",
                principalColumn: "ClassSectionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectAssignments_ClassSection_ClassSectionId",
                table: "SubjectAssignments");

            migrationBuilder.DropTable(
                name: "ClassSection");

            migrationBuilder.DropIndex(
                name: "IX_SubjectAssignments_ClassSectionId",
                table: "SubjectAssignments");

            migrationBuilder.DropColumn(
                name: "ClassSectionId",
                table: "SubjectAssignments");
        }
    }
}
