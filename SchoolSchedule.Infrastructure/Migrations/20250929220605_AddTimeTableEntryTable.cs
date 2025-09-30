using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSchedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeTableEntryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeTableEntries",
                columns: table => new
                {
                    TimeTableEntryId = table.Column<int>(type: "int", nullable: false),
                    TimeTableEntryGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    TeacherAssignmentId = table.Column<int>(type: "int", nullable: true),
                    SubjectAssignmentId = table.Column<int>(type: "int", nullable: false),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    IsPlaceHolder = table.Column<bool>(type: "bit", nullable: false, computedColumnSql: "CASE WHEN [TeacherAssignmentId] IS NULL THEN 1 ELSE 0 END")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeTableEntries", x => x.TimeTableEntryId);
                    table.ForeignKey(
                        name: "FK_TimeTableEntries_SchoolWeeks_DayId",
                        column: x => x.DayId,
                        principalTable: "SchoolWeeks",
                        principalColumn: "SchoolWeekId");
                    table.ForeignKey(
                        name: "FK_TimeTableEntries_SubjectAssignments_SubjectAssignmentId",
                        column: x => x.SubjectAssignmentId,
                        principalTable: "SubjectAssignments",
                        principalColumn: "SubjectAssignmentId");
                    table.ForeignKey(
                        name: "FK_TimeTableEntries_TeacherAssignment_TeacherAssignmentId",
                        column: x => x.TeacherAssignmentId,
                        principalTable: "TeacherAssignment",
                        principalColumn: "TeacherAssignmentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeTableEntries_DayId",
                table: "TimeTableEntries",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTableEntries_SubjectAssignmentId",
                table: "TimeTableEntries",
                column: "SubjectAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTableEntries_TeacherAssignmentId",
                table: "TimeTableEntries",
                column: "TeacherAssignmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeTableEntries");
        }
    }
}
