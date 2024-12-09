using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentSystemUCB.Migrations
{
    /// <inheritdoc />
    public partial class EDPNoIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Recreate SubjectEDPCode without Identity
            migrationBuilder.AddColumn<int>(
                name: "SubjectEDPCode",
                table: "SubjectSchedules",
                nullable: false);

            // Copy data back to SubjectEDPCode from TempKey
            migrationBuilder.Sql("UPDATE SubjectSchedules SET SubjectEDPCode = TempKey");

            // Set SubjectEDPCode as the primary key again
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectSchedules",
                table: "SubjectSchedules");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectSchedules",
                table: "SubjectSchedules",
                column: "SubjectEDPCode");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
