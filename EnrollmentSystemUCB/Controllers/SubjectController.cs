using EnrollmentSystemUCB.Data;
using EnrollmentSystemUCB.Models;
using EnrollmentSystemUCB.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnrollmentSystemUCB.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public SubjectController(ApplicationDbContext dbContext)
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
                StudMInitial = viewModel.StudMInitial ?? " ",
                StudLName = viewModel.StudLName,
                StudCourse = viewModel.StudCourse,
                StudYear = viewModel.StudYear
            };

            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();


            return RedirectToAction("List", "Student");
        }
    }
}
