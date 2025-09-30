using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSchedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSubjectProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FixedDay",
                table: "Subjects");

            migrationBuilder.AddColumn<int>(
                name: "FixedDayId",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FixedPeriod",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsFixed",
                table: "Subjects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReligious",
                table: "Subjects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_FixedDayId",
                table: "Subjects",
                column: "FixedDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_SchoolWeeks_FixedDayId",
                table: "Subjects",
                column: "FixedDayId",
                principalTable: "SchoolWeeks",
                principalColumn: "SchoolWeekId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_SchoolWeeks_FixedDayId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_FixedDayId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "FixedDayId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "FixedPeriod",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "IsFixed",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "IsReligious",
                table: "Subjects");

            migrationBuilder.AddColumn<bool>(
                name: "FixedDay",
                table: "Subjects",
                type: "bit",
                nullable: true,
                defaultValue: false);
        }
    }
}
