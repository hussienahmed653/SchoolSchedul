using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSchedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSchoolWeeksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchoolWeeks",
                columns: table => new
                {
                    SchoolWeekId = table.Column<int>(type: "int", nullable: false),
                    SchoolWeekGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolWeekDay = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolWeeks", x => x.SchoolWeekId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolWeeks");
        }
    }
}
