using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSchedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Classe",
                table: "Classe");

            migrationBuilder.RenameTable(
                name: "Classe",
                newName: "Classes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Classes",
                table: "Classes",
                column: "ClasseId");

            migrationBuilder.CreateTable(
                name: "Departements",
                columns: table => new
                {
                    DepartementId = table.Column<int>(type: "int", nullable: false),
                    DepartementGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClasseId = table.Column<int>(type: "int", nullable: false),
                    DepartementName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departements", x => x.DepartementId);
                    table.ForeignKey(
                        name: "FK_Departements_Classes_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classes",
                        principalColumn: "ClasseId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departements_ClasseId",
                table: "Departements",
                column: "ClasseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Classes",
                table: "Classes");

            migrationBuilder.RenameTable(
                name: "Classes",
                newName: "Classe");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Classe",
                table: "Classe",
                column: "ClasseId");
        }
    }
}
