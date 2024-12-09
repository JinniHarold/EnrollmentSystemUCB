using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentSystemUCB.Migrations
{
    /// <inheritdoc />
    public partial class TimeFormatChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "SubjectTimeStart",
                table: "SubjectSchedules",
                type: "time(0)",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "SubjectTimeEnd",
                table: "SubjectSchedules",
                type: "time(0)",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "SubjectTimeStart",
                table: "SubjectSchedules",
                type: "time",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time(0)");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "SubjectTimeEnd",
                table: "SubjectSchedules",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time(0)");
        }

    }
}
