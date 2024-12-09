using EnrollmentSystemUCB.Data;
using EnrollmentSystemUCB.Models;
using EnrollmentSystemUCB.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EnrollmentSystemUCB.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public EnrollmentController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult AddEnrollment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEnrollment(AddEnrollmentHeader headerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(headerModel); 
            }

            var enrollmentHeader = new EnrollmentHeader
            {
                Id = headerModel.Id,
                StudYear = headerModel.StudYear,
                DateEnrolled = DateTime.Now,
                Encoder = headerModel.Encoder,
                TotalUnits = headerModel.TotalUnits
            };

            await dbContext.EnrollmentHeaders.AddAsync(enrollmentHeader);
            await dbContext.SaveChangesAsync(); 

            return RedirectToAction("List", "Student");
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentNameById(int id)
        {
            var student = await dbContext.Students
                .Where(s => s.Id == id)
                .Select(s => new {
                    Name = $"{s.StudLName}, {s.StudFName} {s.StudMInitial}".Trim(),
                    Course = $"{s.StudCourse}".Trim(),
                    Year = $"{s.StudYear}".Trim()
                })
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true, name = student.Name, course = student.Course, year = student.Year});
        }
        [HttpGet]
        public async Task<IActionResult> GetSubjecttCodeByEDP(int edp)
        {
            var subject = await dbContext.SubjectSchedules
                .Where(s => s.SubjectEDPCode == edp)
                .Select(s => new
                {
                    SubjectCode = s.SubjectCode
                })
                .FirstOrDefaultAsync();

            if (subject == null)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true, code = subject.SubjectCode });
        }
        [HttpGet]
        public async Task<IActionResult> GetSubjectDetails(int edpCode, string subjectCode)
        {
            var subjectSchedule = await dbContext.SubjectSchedules
                .Where(s => s.SubjectEDPCode == edpCode)
                .Select(s => new
                {
                    TimeStart = s.SubjectTimeStart,
                    TimeEnd = s.SubjectTimeEnd,
                    Days = s.SubjectDays
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
                return Json(new { success = false });
            }

            return Json(new
            {
                success = true,
                days = subjectSchedule.Days,
                timeStart = subjectSchedule.TimeStart,
                timeEnd = subjectSchedule.TimeEnd,
                units = subject.Units
            });
        }
        [HttpGet]
        public async Task<IActionResult> GetSubjectScheduleList()
        {
            // Retrieves the SubjectEDPCode and SubjectCode columns from the SubjectSchedules table
            var subjectSchedules = await dbContext.SubjectSchedules
                .Select(s => new
                {
                    EDPCode = s.SubjectEDPCode,
                    Code = s.SubjectCode,
                    Days = s.SubjectDays,
                    TimeStart = s.SubjectTimeStart,
                    TimeEnd = s.SubjectTimeEnd
                })
                .ToListAsync();

            // Check if there are any records
            if (!subjectSchedules.Any())
            {
                return Json(new { success = false, message = "No subject schedules found." });
            }

            // Return the data in JSON format for use on the front end
            return Json(new { success = true, subjectSchedules });
        }

        [HttpPost]
        public async Task<IActionResult> AddEnrollmentDetails([FromBody] AddEnrollmentDetails request)
        {
            // Find the subject based on the SubjectCode to get the SubjectUnits
            var subject = await dbContext.Subjects
                         .FirstOrDefaultAsync(s => s.SubjectCode == request.SubjectCode);

            // If the subject is not found, return an error response
            if (subject == null)
            {
                return Json(new { success = false, message = "Subject not found." });
            }

            // Map the AddEnrollmentDetails view model to the EnrollmentDetails entity
            var enrollmentDetails = new EnrollmentDetails
            {
                StudentId = request.StudentId,
                SubjectEDPCode = request.SubjectEDPCode,
                SubjectCode = request.SubjectCode,
                SubjectUnits = subject.SubjectUnits // Use the units retrieved from the Subjects table
            };

            // Add the new enrollment details to the database
            dbContext.EnrollmentDetails.Add(enrollmentDetails);

            subject.SubjClassSize += 1;

            await dbContext.SaveChangesAsync();

            // Return a success response
            return Json(new { success = true });
        }


        [HttpPost]
        public async Task<IActionResult> RemoveSubject(long studentId, string edpCode)
        {
            var enrollment = await dbContext.EnrollmentDetails
                .FirstOrDefaultAsync(d => d.StudentId == studentId && d.SubjectEDPCode.ToString() == edpCode);

            if (enrollment == null)
            {
                return Json(new { success = false, message = "Enrollment not found" });
            }

            // Retrieve the subject by SubjectCode to decrement the class size
            var subject = await dbContext.Subjects
                .FirstOrDefaultAsync(s => s.SubjectCode == enrollment.SubjectCode);

            if (subject == null)
            {
                return Json(new { success = false, message = "Subject not found." });
            }

            dbContext.EnrollmentDetails.Remove(enrollment);
            subject.SubjClassSize -= 1;

            await dbContext.SaveChangesAsync();

            return Json(new { success = true });
        }


        [HttpGet]
        public async Task<IActionResult> IsStudentEnrolled(int studentId)
        {
            // Check if the student exists in EnrollmentHeaders by Id
            var studentExists = await dbContext.EnrollmentHeaders
                                               .AnyAsync(e => e.Id == studentId);

            return Json(new { isEnrolled = studentExists });
        }

    }
}
