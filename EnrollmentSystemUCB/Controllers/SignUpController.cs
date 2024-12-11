using EnrollmentSystemUCB.Data;
using EnrollmentSystemUCB.Models;
using EnrollmentSystemUCB.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnrollmentSystemUCB.Controllers
{
    public class SignUpController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public SignUpController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult StudentInformation()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> StudentInformation(AddStudentViewModel viewModel)
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

            return RedirectToAction("StudentEnrollment", "SignUp", new { id = student.Id });
        }

        [HttpGet]
        public async Task<IActionResult> StudentEnrollment(int id)
        {
            // Retrieve the student information based on the ID
            var student = await dbContext.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
            {
                return NotFound("Student not found.");
            }

            // Format the student name as "[Last name], [First name] [Middle name]"
            string studentName = $"{student.StudLName}, {student.StudFName} {student.StudMInitial}";

            // Pass the details to the view
            ViewData["StudentId"] = id;
            ViewData["StudentName"] = studentName;
            ViewData["StudentYear"] = student.StudYear;
            ViewData["StudentCourse"] = student.StudCourse;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StudentEnrollment(AddEnrollmentHeader headerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(headerModel);
            }

            var enrollmentHeader = new EnrollmentHeader
            {
                Id = headerModel.Id, // The ID will be passed here
                StudYear = headerModel.StudYear,
                DateEnrolled = DateTime.Now,
                Encoder = headerModel.Encoder,
                TotalUnits = headerModel.TotalUnits
            };

            await dbContext.EnrollmentHeaders.AddAsync(enrollmentHeader);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("StudentLogin", "Login");
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

    }
}
