using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentSystemUCB.Migrations
{
    /// <inheritdoc />
    public partial class Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Rename column from SubjMaxStuds to SubjClassSize
            migrationBuilder.RenameColumn(
                name: "SubjMaxStuds",
                table: "Subjects",
                newName: "SubjClassSize");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Rename column back from SubjClassSize to SubjMaxStuds (for rollback purposes)
            migrationBuilder.RenameColumn(
                name: "SubjClassSize",
                table: "Subjects",
                newName: "SubjMaxStuds");

        }
    }
}
