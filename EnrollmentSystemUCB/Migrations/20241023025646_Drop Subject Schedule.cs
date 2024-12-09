using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentSystemUCB.Migrations
{
    /// <inheritdoc />
    public partial class DropSubjectSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectSchedules");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Optionally, restore the table if the migration is rolled back.
            migrationBuilder.CreateTable(
                name: "SubjectSchedules",
                columns: table => new
                {
                    SubjectEDPCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectCode = table.Column<string>(nullable: false),
                    SubjectDays = table.Column<string>(nullable: false),
                    SubjectSY = table.Column<string>(nullable: false),
                    SubjectSection = table.Column<string>(nullable: false),
                    SubjectTimeStart = table.Column<TimeSpan>(nullable: false),
                    SubjectTimeEnd = table.Column<TimeSpan>(nullable: false),
                    SUbjectDescription = table.Column<string>(nullable: false)
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
        }

    }
}
