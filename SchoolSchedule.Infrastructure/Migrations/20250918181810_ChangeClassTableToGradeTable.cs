using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSchedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeClassTableToGradeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departements_Classes_ClasseId",
                table: "Departements");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectAssignments_Classes_ClasseId",
                table: "SubjectAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectAssignments_Departements_DepartementId",
                table: "SubjectAssignments");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.RenameColumn(
                name: "ClasseId",
                table: "SubjectAssignments",
                newName: "GradeId");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectAssignments_SubjectId_ClasseId_DepartementId_EvenOrOdd_Amount",
                table: "SubjectAssignments",
                newName: "IX_SubjectAssignments_SubjectId_GradeId_DepartementId_EvenOrOdd_Amount");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectAssignments_ClasseId",
                table: "SubjectAssignments",
                newName: "IX_SubjectAssignments_GradeId");

            migrationBuilder.RenameColumn(
                name: "ClasseId",
                table: "Departements",
                newName: "GradeId");

            migrationBuilder.RenameIndex(
                name: "IX_Departements_ClasseId",
                table: "Departements",
                newName: "IX_Departements_GradeId");

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    GradeGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    GradeYear = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumberOfGrades = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.GradeId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Departements_Grades_GradeId",
                table: "Departements",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectAssignments_Departements_DepartementId",
                table: "SubjectAssignments",
                column: "DepartementId",
                principalTable: "Departements",
                principalColumn: "DepartementId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectAssignments_Grades_GradeId",
                table: "SubjectAssignments",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "GradeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departements_Grades_GradeId",
                table: "Departements");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectAssignments_Departements_DepartementId",
                table: "SubjectAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectAssignments_Grades_GradeId",
                table: "SubjectAssignments");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.RenameColumn(
                name: "GradeId",
                table: "SubjectAssignments",
                newName: "ClasseId");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectAssignments_SubjectId_GradeId_DepartementId_EvenOrOdd_Amount",
                table: "SubjectAssignments",
                newName: "IX_SubjectAssignments_SubjectId_ClasseId_DepartementId_EvenOrOdd_Amount");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectAssignments_GradeId",
                table: "SubjectAssignments",
                newName: "IX_SubjectAssignments_ClasseId");

            migrationBuilder.RenameColumn(
                name: "GradeId",
                table: "Departements",
                newName: "ClasseId");

            migrationBuilder.RenameIndex(
                name: "IX_Departements_GradeId",
                table: "Departements",
                newName: "IX_Departements_ClasseId");

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClasseId = table.Column<int>(type: "int", nullable: false),
                    ClasseGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ClasseYear = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumberOfClasses = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClasseId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Departements_Classes_ClasseId",
                table: "Departements",
                column: "ClasseId",
                principalTable: "Classes",
                principalColumn: "ClasseId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectAssignments_Classes_ClasseId",
                table: "SubjectAssignments",
                column: "ClasseId",
                principalTable: "Classes",
                principalColumn: "ClasseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectAssignments_Departements_DepartementId",
                table: "SubjectAssignments",
                column: "DepartementId",
                principalTable: "Departements",
                principalColumn: "DepartementId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
