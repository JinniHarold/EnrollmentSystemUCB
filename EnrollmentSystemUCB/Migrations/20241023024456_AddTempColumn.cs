using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentSystemUCB.Migrations
{
    /// <inheritdoc />
    public partial class AddTempColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add a new temporary column
            migrationBuilder.AddColumn<int>(
                name: "TempKey",
                table: "SubjectSchedules",
                nullable: false,
                defaultValue: 0);

            // Copy data from the old primary key column
            migrationBuilder.Sql("UPDATE SubjectSchedules SET TempKey = SubjectEDPCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
