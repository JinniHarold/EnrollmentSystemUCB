using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentSystemUCB.Migrations
{
    /// <inheritdoc />
    public partial class DropEdpCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the primary key constraint from SubjectEDPCode
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectSchedules",
                table: "SubjectSchedules");

            // Drop the SubjectEDPCode column
            migrationBuilder.DropColumn(
                name: "SubjectEDPCode",
                table: "SubjectSchedules");

            // Set TempKey as the temporary primary key
            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectSchedules",
                table: "SubjectSchedules",
                column: "TempKey");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
