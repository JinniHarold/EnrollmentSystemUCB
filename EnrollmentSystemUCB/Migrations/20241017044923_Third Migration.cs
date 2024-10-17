using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentSystemUCB.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubjectSchedules",
                columns: table => new
                {
                    SubjectEDPCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SUbjectDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectTimeStart = table.Column<TimeOnly>(type: "time", nullable: false),
                    SubjectTimeEnd = table.Column<TimeOnly>(type: "time", nullable: false),
                    SubjectDays = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectSection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectSY = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectSchedules", x => x.SubjectEDPCode);
                    table.ForeignKey(
                        name: "FK_SubjectSchedules_Subjects_SubjectCode",
                        column: x => x.SubjectCode,
                        principalTable: "Subjects",
                        principalColumn: "SubjectCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectSchedules_SubjectCode",
                table: "SubjectSchedules",
                column: "SubjectCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectSchedules");
        }
    }
}
