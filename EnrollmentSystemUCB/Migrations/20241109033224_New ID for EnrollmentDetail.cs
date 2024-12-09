using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentSystemUCB.Migrations
{
    /// <inheritdoc />
    public partial class NewIDforEnrollmentDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrollmentDetails",
                table: "EnrollmentDetails");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "EnrollmentDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrollmentDetails",
                table: "EnrollmentDetails",
                columns: new[] { "StudentId", "SubjectEDPCode" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrollmentDetails",
                table: "EnrollmentDetails");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "EnrollmentDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrollmentDetails",
                table: "EnrollmentDetails",
                columns: new[] { "Id", "SubjectEDPCode" });
        }
    }
}
