using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentSystemUCB.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectUnits = table.Column<int>(type: "int", nullable: false),
                    SubjectCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectOffering = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectCourse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectCurrYear = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectCode);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
