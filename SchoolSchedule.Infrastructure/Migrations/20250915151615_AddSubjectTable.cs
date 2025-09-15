using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSchedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSubjectTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "DepartementGuid",
                table: "Departements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClasseGuid",
                table: "Classes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    SubjectGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    SubjectName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "SubjectAssignments",
                columns: table => new
                {
                    SubjectAssignmentId = table.Column<int>(type: "int", nullable: false),
                    SubjectAssignmentGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    ClasseId = table.Column<int>(type: "int", nullable: false),
                    DepartementId = table.Column<int>(type: "int", nullable: false),
                    EvenOrOdd = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectAssignments", x => x.SubjectAssignmentId);
                    table.ForeignKey(
                        name: "FK_SubjectAssignments_Classes_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classes",
                        principalColumn: "ClasseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectAssignments_Departements_DepartementId",
                        column: x => x.DepartementId,
                        principalTable: "Departements",
                        principalColumn: "DepartementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectAssignments_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectAssignments_ClasseId",
                table: "SubjectAssignments",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectAssignments_DepartementId",
                table: "SubjectAssignments",
                column: "DepartementId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectAssignments_SubjectId_ClasseId_DepartementId_EvenOrOdd_Amount",
                table: "SubjectAssignments",
                columns: new[] { "SubjectId", "ClasseId", "DepartementId", "EvenOrOdd", "Amount" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectAssignments");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.AlterColumn<Guid>(
                name: "DepartementGuid",
                table: "Departements",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClasseGuid",
                table: "Classes",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");
        }
    }
}
