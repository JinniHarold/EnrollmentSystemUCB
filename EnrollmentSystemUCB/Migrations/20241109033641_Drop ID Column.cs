using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentSystemUCB.Migrations
{
    /// <inheritdoc />
    public partial class DropIDColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id", // The column you want to drop
                table: "EnrollmentDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EnrollmentDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
