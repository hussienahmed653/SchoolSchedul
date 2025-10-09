using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSchedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPeriodRange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPlaceHolder",
                table: "TimeTableEntries");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectAssignmentId",
                table: "TimeTableEntries",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Period",
                table: "TimeTableEntries",
                type: "int",
                nullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_TimeTableEntry_PeriodRange",
                table: "TimeTableEntries",
                sql: "Period >= 1 AND Period <= 8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_TimeTableEntry_PeriodRange",
                table: "TimeTableEntries");

            migrationBuilder.DropColumn(
                name: "Period",
                table: "TimeTableEntries");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectAssignmentId",
                table: "TimeTableEntries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPlaceHolder",
                table: "TimeTableEntries",
                type: "bit",
                nullable: false,
                computedColumnSql: "CASE WHEN [TeacherAssignmentId] IS NULL THEN 1 ELSE 0 END");
        }
    }
}
