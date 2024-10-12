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
        public IActionResult AddSubject()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSubject(AddSubjectViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var subject = new Subject
            {
                SubjectCode = viewModel.SubjectCode,
                SubjectDescription = viewModel.SubjectDescription,
                SubjectUnits = viewModel.SubjectUnits,
                SubjectOffering = viewModel.SubjectOffering,
                SubjectCategory = viewModel.SubjectCategory,
                SubjectCourse = viewModel.SubjectCourse,
                SubjectCurrYear = viewModel.SubjectCurrYear,
            };

            await dbContext.Subjects.AddAsync(subject);
            await dbContext.SaveChangesAsync();

            return View();
        }
    }
}
