using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentSystemUCB.Migrations
{
    /// <inheritdoc />
    public partial class AddSTUDENTS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            // Create the new table with int Id
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),  // Auto-incrementing ID
                    StudLName = table.Column<string>(nullable: false),
                    StudFName = table.Column<string>(nullable: false),
                    StudMInitial = table.Column<string>(nullable: true),
                    StudCourse = table.Column<string>(nullable: false),
                    StudYear = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Recreate the old table if needed in the down migration
            migrationBuilder.DropTable(name: "Students");
        }
    }
}
