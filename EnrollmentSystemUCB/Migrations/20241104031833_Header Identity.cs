using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentSystemUCB.Migrations
{
    /// <inheritdoc />
    public partial class HeaderIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the existing table if it exists
            migrationBuilder.DropTable(name: "EnrollmentHeaders");

            // Recreate the EnrollmentHeaders table with the correct identity configuration
            migrationBuilder.CreateTable(
                name: "EnrollmentHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false), // No identity here
                    EnrollmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"), // This is now the identity column
                    StudYear = table.Column<int>(type: "int", nullable: false),
                    DateEnrolled = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Encoder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalUnits = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentHeaders", x => x.Id); // Keep Id as the primary key
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "EnrollmentHeaders");
        }
    }
}
