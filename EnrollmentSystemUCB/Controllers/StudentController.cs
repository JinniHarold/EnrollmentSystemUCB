using EnrollmentSystemUCB.Data;
using EnrollmentSystemUCB.Models;
using EnrollmentSystemUCB.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnrollmentSystemUCB.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Generate the custom ID
            int generatedID = GenerateID();

            var student = new Student
            {
                Id = generatedID, // Set the generated ID here
                StudFName = viewModel.StudFName,
                StudMInitial = viewModel.StudMInitial ?? " ",
                StudLName = viewModel.StudLName,
                StudCourse = viewModel.StudCourse,
                StudYear = viewModel.StudYear,
                Password = viewModel.Password
            };

            // Use a transaction to safely enable and disable IDENTITY_INSERT
            using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Enable IDENTITY_INSERT
                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Students ON");

                    await dbContext.Students.AddAsync(student);
                    await dbContext.SaveChangesAsync();

                    // Disable IDENTITY_INSERT
                    await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Students OFF");

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            return RedirectToAction("List", "Student");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync();
            return View(students);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var student = await dbContext.Students.FindAsync(Id);

            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await dbContext.Students.FindAsync(viewModel.Id);

            if (student is not null)
            {
                student.StudFName = viewModel.StudFName;
                student.StudMInitial = viewModel.StudMInitial ?? " ";
                student.StudLName = viewModel.StudLName;
                student.StudCourse = viewModel.StudCourse;
                student.StudYear = viewModel.StudYear;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Student");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel) 
        {
            var student = await dbContext.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (student is not null)
            {
                // Delete associated records from EnrollmentHeaders
                var enrollmentHeaders = dbContext.EnrollmentHeaders.Where(eh => eh.Id == student.Id);
                dbContext.EnrollmentHeaders.RemoveRange(enrollmentHeaders);

                // Delete associated records from EnrollmentDetails
                var enrollmentDetails = dbContext.EnrollmentDetails.Where(ed => ed.StudentId == student.Id);
                dbContext.EnrollmentDetails.RemoveRange(enrollmentDetails);

                dbContext.Students.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Student");
        }

        private int GenerateID()
        {
            Random random = new Random();
            int newID;
            do
            {
                // Generate a random 6-digit number
                newID = random.Next(20000000, 29999999);

            } while (dbContext.Students.Any(s => s.Id == newID));

            return newID;
        }


        [HttpGet]
        public async Task<IActionResult> GetEnrolledSubjects(int studentId)
        {
            // Fetch enrollment details for the student
            var enrolledSubjects = await dbContext.EnrollmentDetails
                .Where(ed => ed.StudentId == studentId)
                .Select(ed => new
                {
                    ed.SubjectEDPCode,
                    ed.SubjectCode
                })
                .ToListAsync();

            // Fetch total units from EnrollmentHeader (assuming 1 record per student)
            var totalUnits = await dbContext.EnrollmentHeaders
                .Where(eh => eh.Id == studentId)
                .Select(eh => eh.TotalUnits)
                .FirstOrDefaultAsync();

            // Create a list to store detailed subject information
            var subjectDetailsList = new List<object>();

            // Retrieve details for each enrolled subject
            foreach (var enrolledSubject in enrolledSubjects)
            {
                var subjectDetails = await GetSubjectDetails(enrolledSubject.SubjectEDPCode, enrolledSubject.SubjectCode);
                if (subjectDetails.success)
                {
                    subjectDetailsList.Add(new
                    {
                        enrolledSubject.SubjectEDPCode,
                        enrolledSubject.SubjectCode,
                        Description = subjectDetails.description,
                        Schedule = $"{subjectDetails.timeStart} - {subjectDetails.timeEnd}",
                        Units = subjectDetails.units,
                        Days = subjectDetails.days
                    });
                }
            }

            return Json(new { EnrolledSubjects = subjectDetailsList, TotalUnits = totalUnits});
        }
        public async Task<dynamic> GetSubjectDetails(int edpCode, string subjectCode)
        {
            var subjectSchedule = await dbContext.SubjectSchedules
                .Where(s => s.SubjectEDPCode == edpCode)
                .Select(s => new
                {
                    TimeStart = s.SubjectTimeStart,
                    TimeEnd = s.SubjectTimeEnd,
                    Days = s.SubjectDays,
                    Description = s.SUbjectDescription
                })
                .FirstOrDefaultAsync();

            var subject = await dbContext.Subjects
                .Where(s => s.SubjectCode == subjectCode)
                .Select(s => new
                {
                    Units = s.SubjectUnits
                })
                .FirstOrDefaultAsync();

            if (subjectSchedule == null || subject == null)
            {
                return new { success = false };
            }

            return new
            {
                success = true,
                days = subjectSchedule.Days,
                timeStart = subjectSchedule.TimeStart,
                timeEnd = subjectSchedule.TimeEnd,
                units = subject.Units,
                description = subjectSchedule.Description
            };
        }
    }
}
