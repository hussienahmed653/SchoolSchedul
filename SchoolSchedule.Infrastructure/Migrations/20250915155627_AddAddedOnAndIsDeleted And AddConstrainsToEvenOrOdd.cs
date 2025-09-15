using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSchedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAddedOnAndIsDeletedAndAddConstrainsToEvenOrOdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "SubjectAssignments",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "SubjectAssignments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SubjectAssignments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddCheckConstraint(
                name: "CK_SubjectAssignments_EvenOrOdd",
                table: "SubjectAssignments",
                sql: "[EvenOrOdd] IN (1, 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedOn",
                table: "SubjectAssignments");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "SubjectAssignments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SubjectAssignments");

            migrationBuilder.DropCheckConstraint(
                name: "CK_SubjectAssignments_EvenOrOdd",
                table: "SubjectAssignments");
        }
    }
}
