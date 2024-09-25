using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentSystemUCB.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudLName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudFName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudMInitial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudCourse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
