using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentSystemUCB.Migrations
{
    /// <inheritdoc />
    public partial class CreateSubjectSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubjectSchedules",
                columns: table => new
                {
                    SubjectEDPCode = table.Column<int>(nullable: false),
                    SubjectCode = table.Column<string>(nullable: false),
                    SubjectDays = table.Column<string>(nullable: false),
                    SubjectSY = table.Column<string>(nullable: false),
                    SubjectSection = table.Column<string>(nullable: false),
                    SubjectTimeStart = table.Column<TimeSpan>(nullable: false),
                    SubjectTimeEnd = table.Column<TimeSpan>(nullable: false),
                    SUbjectDescription = table.Column<string>(nullable: false),
                    // Add other columns as needed
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectSchedules");
        }

    }
}
