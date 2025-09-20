using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSchedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateWorkType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHECK_IF_WORKTYPE_IS_FULLTIME_OR_PARTTIME",
                table: "Teachers");

            migrationBuilder.AddCheckConstraint(
                name: "CHECK_IF_WORKTYPE_IS_FULLTIME_OR_PARTTIME",
                table: "Teachers",
                sql: "[WorkType] IN ('كلي','جزئي')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHECK_IF_WORKTYPE_IS_FULLTIME_OR_PARTTIME",
                table: "Teachers");

            migrationBuilder.AddCheckConstraint(
                name: "CHECK_IF_WORKTYPE_IS_FULLTIME_OR_PARTTIME",
                table: "Teachers",
                sql: "[WorkType] IN ('كامل','جزئي')");
        }
    }
}
