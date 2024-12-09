using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentSystemUCB.Migrations
{
    /// <inheritdoc />
    public partial class EnrollmentHeaderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create EnrollmentHeaders table
            migrationBuilder.CreateTable(
                name: "EnrollmentHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"), // Identity column
                    StudYear = table.Column<int>(nullable: false),
                    DateEnrolled = table.Column<DateTime>(nullable: false), // For DateOnly, use DateTime in database
                    Encoder = table.Column<string>(nullable: false),
                    TotalUnits = table.Column<double>(nullable: false, defaultValue: 0.0) // Set default value to 0.0
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentHeaders", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the table if rollback happens
            migrationBuilder.DropTable(
                name: "EnrollmentHeaders");
        }
    }
}
