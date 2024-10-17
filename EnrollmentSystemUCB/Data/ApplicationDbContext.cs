using Microsoft.EntityFrameworkCore;
using EnrollmentSystemUCB.Models.Entities;

namespace EnrollmentSystemUCB.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectSchedule> SubjectSchedules { get; set; }
    }
}
