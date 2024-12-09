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
        public DbSet<EnrollmentHeader> EnrollmentHeaders { get; set; }
        public DbSet<EnrollmentDetails> EnrollmentDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EnrollmentDetails>()
                .HasKey(e => new { e.StudentId, e.SubjectEDPCode });

            // Configure EnrollmentId to be an identity column
            modelBuilder.Entity<EnrollmentHeader>()
                .Property(e => e.EnrollmentId)
                .ValueGeneratedOnAdd(); // This makes it an identity column

            // Ensure Id is not treated as an identity column
            modelBuilder.Entity<EnrollmentHeader>()
                .Property(e => e.Id)
                .ValueGeneratedNever(); // This makes it not use identity
        }

    }
}
