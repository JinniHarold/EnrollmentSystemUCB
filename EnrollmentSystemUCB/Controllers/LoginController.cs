using Microsoft.AspNetCore.Mvc;
using EnrollmentSystemUCB.Data;
using EnrollmentSystemUCB.Models;
using EnrollmentSystemUCB.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EnrollmentSystemUCB.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public LoginController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult AdminLogin(string username, string password)
        {
            if (username == "Admin" && password == "aDmin123")
            {
                // Redirect to the desired action and controller
                return RedirectToAction("AddEnrollment", "Enrollment");
            }

            // If login fails, display an error message
            ViewBag.ErrorMessage = "Invalid username or password.";
            return View();
        }
        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpGet]   
        public IActionResult StudentLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult StudentLogin(string studentId, string password)
        {
            var student = dbContext.Students
                        .FirstOrDefault(s => s.Id.ToString() == studentId && s.Password == password);

            if (student != null)
            {
                // Credentials are correct, redirect to the StudyLoad page
                return RedirectToAction("StudyLoad", "Login", new { Id = student.Id });
            }
            else
            {
                // Credentials are invalid, stay on the login page with an error
                ModelState.AddModelError("", "Invalid login attempt.");
                return View();
            }
        }
        [HttpGet("StudyLoad/{id}")]
        public async Task<IActionResult> StudyLoad(int Id)
        {
            var student = await dbContext.Students.FindAsync(Id);

            return View(student);
        }

        [HttpPost("StudyLoad/{id}")]
        public async Task<IActionResult> StudyLoad(Student viewModel)
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

            return Json(new { EnrolledSubjects = subjectDetailsList, TotalUnits = totalUnits });
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

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
