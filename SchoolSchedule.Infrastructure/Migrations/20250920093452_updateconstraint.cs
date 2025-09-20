using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSchedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateconstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHECK_IF_WORKTYPE_IS_FULLTIME_OR_PARTTIME",
                table: "Teachers");

            migrationBuilder.AlterColumn<string>(
                name: "WorkType",
                table: "Teachers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddCheckConstraint(
                name: "CHECK_IF_WORKTYPE_IS_FULLTIME_OR_PARTTIME",
                table: "Teachers",
                sql: "[WorkType] IN (N'كلي',N'جزئي')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHECK_IF_WORKTYPE_IS_FULLTIME_OR_PARTTIME",
                table: "Teachers");

            migrationBuilder.AlterColumn<string>(
                name: "WorkType",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddCheckConstraint(
                name: "CHECK_IF_WORKTYPE_IS_FULLTIME_OR_PARTTIME",
                table: "Teachers",
                sql: "[WorkType] IN ('كلي','جزئي')");
        }
    }
}
