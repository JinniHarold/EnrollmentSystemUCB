using EnrollmentSystemUCB.Data;
using EnrollmentSystemUCB.Models;
using EnrollmentSystemUCB.Models.Entities;
using Microsoft.AspNetCore.Mvc;

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

            var student = new Student
            {
                StudFName = viewModel.StudFName,
                StudMInitial = viewModel.StudMInitial,
                StudLName = viewModel.StudLName,
                StudCourse = viewModel.StudCourse,
                StudYear = viewModel.StudYear
            };

            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();

            
            return RedirectToAction("Index", "Home");
        }
    }
}
